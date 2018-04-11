//Yebai Zhao
/*
 * This Script doesn't attach to any object in Unity. So it is harder to tune the game values 
 * However, every other script can access this one in a much easier way.
 *
 * Managing game data such as time
 * Getting Controller input
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HiddenGameManager : Singleton<HiddenGameManager> {
	public delegate void HGMEvent(string tag);
	public static event HGMEvent DysonClean;
	private bool sessionLive = true;
	/*Time
	//private float _timeRemaining;
	//public float maxGameTime = 300; //seconds.
	//public float TimeRemaining//the time remaining from the begining of the game in sec.
	{
		get { return _timeRemaining; }
		set { _timeRemaining = value; }
	}*/

	//Data Writer
	public static string[] dataArray = {
		"Time","HeadX","HeadY","HeadZ","LHandX","LHandY","LHandZ","RHandX","RHandY","RHandZ",//0~9
		"HeadtoBoard","HeadtoCat","Score","CatLoc","CatEvent","LHTrigger","LRTrigger","LHGrabb","RHGrabb", "TColor", //10~19
		"Text","CubeEntered","Bonus","CatX" ,"CatY" ,"CatZ","CatType","ForwardX","ForwardY","ForwardZ","VGType"};//20~30
	private float nextCSVtime= 0f;
	private float CSVperiod= 0.1f;
	private Vector3 headToBorad;
	public Vector3 headToCat;
	private Vector3 boardposition;
	private GameObject head;
	private GameObject lefthand;
	private GameObject righthand;
	private string destination;

	//Thumb
	public string thumbstickStatus = "null";
	//Cat
	public bool catHide = false;// if the cat is hiding or not
	public float catMinHideTime = 10f;//The cat will at least hide for this time after shoot.
	public float catHidingFor = 20f;//Each time cat will hide for different length
	public float catTeleportTime = 0f;
	public float catBirthTime =0f;
	public float catDeathTime = 0f;
	public float uiReportTime = 0f;
	public Vector3 catLocation ;
	private GameObject[] inCatTags;
	private CatMovment catScript;
	private string[] catTypeArray = {"CubeTaker", "Good Cat", "ScoreTaker","ScoreGiver"};
	//private float teleNextPeriod = 0f;
	private int[][] randomMatrix ={//Each array in the matrix contains 0~14, onec the VG selected, the Cat will TP in that certain sequence 
		new int[]{14,5,12,11,4,6,2,1,13,10,0,7,3,9,8},
		new int[]{6,12,5,9,4,2,3,11,1,8,7,10,14,0,13},
		new int[]{9,7,4,2,8,6,12,5,14,13,11,3,10,1,0},
		new int[]{11,14,10,9,7,2,6,0,1,3,4,12,13,5,8},
		new int[]{3,2,7,10,6,13,5,0,8,9,11,4,14,12,1},
		new int[]{8,3,14,10,6,1,4,5,7,0,13,9,11,2,12},
		new int[]{6,10,1,11,0,7,13,12,4,5,3,2,8,14,9},
		new int[]{13,2,1,12,5,9,8,11,14,6,4,7,0,3,10}
	};
	private int catKilledtimes = 0;


	//UI
	public bool holdVG = false;//Dont show visual guidance when true



	//Maintask
	public int playerScore = 0;
	public Vector3 cubeHome = new Vector3(41.29f, 4.52f, 35.9f);


	//2ndTask
	public List<float> reportTimeList = new List<float>();
	public float total_UGTime = 0f;
	public float mean_UGTime = 0f;

	void OnEnable(){//subscribe event
		LaserControll.LaserHitCat += CatDeath;
	}
	void OnDisable(){
		LaserControll.LaserHitCat -= CatDeath;
	}
	/// <Vars>
	/// 
	/// /////////////////////////////////////////////////////////////////////////////////////
	/// 
	/// </Main sequence>
	public override void Awake(){
		destination = Application.persistentDataPath +"/"+System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
		LogToCSV ();
		dataArray [13] = "";
		dataArray [26] = "";
	}
	void Start () {

		//TimeRemaining = maxGameTime;
		inCatTags = GameObject.FindGameObjectsWithTag ("Unique");////It cant catch inactive objs, so put in Start()
		head = GameObject.FindGameObjectWithTag ("MainCamera");
		lefthand = GameObject.FindGameObjectWithTag ("lefthand");
		righthand = GameObject.FindGameObjectWithTag ("righthand");
		boardposition =new Vector3 (40.856f, 4.716515f, 36.1156f);

		catScript = inCatTags[0].GetComponent<CatMovment> ();
		inCatTags[0].SetActive(false);
		catHide = true;
	}

	// Update is called once per frame
	void Update () {
		//TimeRemaining -= Time.deltaTime;

		//thumbstickStatus = GetThumbstickStatus(); - obseloete

		if (catKilledtimes == 15) {
			if (DysonClean != null) {
				DysonClean ("Session Ended");
			}//the end of the world ass we know it
			sessionLive = false;
			catKilledtimes = 0;
		}

		if ((Time.realtimeSinceStartup - catDeathTime) > catHidingFor && catHide && sessionLive) {
			CatRebirth ();
		}

		if (Time.realtimeSinceStartup > nextCSVtime) {
			LogData ();
			LogToCSV ();
			nextCSVtime += CSVperiod;
		}

		/*if (Time.realtimeSinceStartup > teleNextPeriod && !catHide ) { // After certain amount of time the cat will TP again
			catScript.CatTeleport ();
			teleNextPeriod = teleNextPeriod+ 20.0f + Random.Range(-5,10);
		}	*/

		if(Input.GetKey("escape")){ // quit game under these circumstances
			Application.Quit();
		}


	}


	// <End of the Update>
	/////////////////////////////////////////////////////////////////////////
	// </summary>
	// <returns>The thumbstick status.</returns>

	/*public static float GaussianDistr(float mean, float stdDev){
		//Random rand = new Random(); //reuse this if you are generating many
		float u1 = 1.0-Random.value; //uniform(0,1] random doubles
		float u2 = 1.0-Random.value;
		float randStdNormal = Mathf.Sqrt(-2.0 * Mathf.Log(u1)) *
			Mathf.Sin(2.0 * Mathf.PI * u2); //random normal(0,1)
		float randNormal =
			mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
		return randNormal;
	}*/



	public void CatRebirth(){
		int i;
		if (int.TryParse (dataArray [30], out i)) {
			catHide = false;
			foreach (GameObject obj in inCatTags) {
				obj.SetActive (true);
			}
			//catScript.CatOnDesk ();
			catScript.CatTeleport(randomMatrix[i] [catKilledtimes] );
			catBirthTime = Time.realtimeSinceStartup;
			//teleNextPeriod = catBirthTime;
			dataArray [26] = catTypeArray [Random.Range (0, catTypeArray.Length)];//
			LogEvent (13, 14, "WaitForTP", "CatRebirth");
		}else Debug.Log("Fail to get the cat resurrected");


	}



	private void CatDeath(){
		catDeathTime = Time.realtimeSinceStartup;
		if (dataArray [26] == "ScoreTaker") {
			playerScore += Mathf.RoundToInt (500/(catDeathTime - catBirthTime+10)-50);

		} else if (dataArray [26] == "ScoreGiver") {
			playerScore += Mathf.RoundToInt (500/(catDeathTime - catBirthTime+10)+50);
		}
		LogEvent (13, 14, "", "CatDead");
		dataArray [26] = "";//tpye
		catHide = true;
		catHidingFor = catMinHideTime + Random.Range (0, 10);
		catKilledtimes++;
}



	public void CalulateReaction(){
		total_UGTime = 0f;
		foreach (float t in reportTimeList) {
			total_UGTime += t;
		}
		mean_UGTime = total_UGTime / reportTimeList.Count;
	}
	
	public void LogEvent(int i, string message){
		dataArray [i] = message;
		dataArray [0] = Time.realtimeSinceStartup.ToString();
		LogToCSV ();
	}
	public void LogEvent(int i, int p, string message1, string message2){
		dataArray [i] = message1;
		dataArray [p] = message2;
		dataArray [0] = Time.realtimeSinceStartup.ToString();
		LogToCSV ();
	}
	public void LogEvent(int i, int p, int q, string message1, string message2, string message3){
		dataArray [i] = message1;
		dataArray [p] = message2;
		dataArray [q] = message3;
		dataArray [0] = Time.realtimeSinceStartup.ToString();
		LogToCSV ();
	}
	
	private void LogData(){
		dataArray [0] = Time.realtimeSinceStartup.ToString();//Time
		dataArray [1] = head.transform.position.x.ToString();//Headx
		dataArray [2] = head.transform.position.y.ToString();//Heady
		dataArray [3] = head.transform.position.z.ToString();//HeadZ
		dataArray [4] = lefthand.transform.position.x.ToString();//LhandX
		dataArray [5] = lefthand.transform.position.y.ToString();//LhandY
		dataArray [6] = lefthand.transform.position.z.ToString();//LhandZ
		dataArray [7] = righthand.transform.position.x.ToString();//RhandX
		dataArray [8] = righthand.transform.position.y.ToString();//RhandY
		dataArray [9] = righthand.transform.position.z.ToString();//RhandZ
		headToBorad = boardposition - head.transform.position;//calulating 
		headToCat = catLocation - head.transform.position;//
		dataArray [10] = Vector3.Angle (head.transform.forward, headToBorad).ToString();//Headtoborad
		dataArray [11] = catHide? "":Vector3.Angle (head.transform.forward, headToCat).ToString();//Headtocat
		dataArray [12] = playerScore.ToString();//score
		dataArray [14] = "";//Catevent
		dataArray [15] = OVRInput.Get (OVRInput.Button.PrimaryIndexTrigger)? "true":"";
		dataArray [16] = OVRInput.Get (OVRInput.Button.SecondaryIndexTrigger)? "true":"";
		dataArray [17] = OVRInput.Get (OVRInput.Button.PrimaryHandTrigger)? "true":"";
		dataArray [18] = OVRInput.Get (OVRInput.Button.SecondaryHandTrigger)? "true":"";
		dataArray [21] = "";//Cube
		dataArray [23] = catHide ? "" : catLocation.x.ToString ();
        dataArray [24] = catHide ? "" : catLocation.y.ToString();
        dataArray [25] = catHide ? "" : catLocation.z.ToString();
        dataArray [27] = head.transform.forward.x.ToString ();
		dataArray [28] = head.transform.forward.y.ToString ();
		dataArray [29] = head.transform.forward.z.ToString ();
	}
	private void LogToCSV(){ //Every other reference should use the WriteMessageToArray method
		StreamWriter datawriter = new StreamWriter (destination + "data.csv", true);
		string linetext = null;

		foreach (string f in dataArray) {
			linetext = linetext + f + ",";
		}
		linetext = linetext.Remove (linetext.Length - 1);
		datawriter.WriteLine (linetext);
		datawriter.Close ();
	}
	/*private void WriteToCSV(){
		StreamWriter datawriter = new StreamWriter (destination + "data.csv", true);
		string linetext = null;

		foreach (string f in dataArray) {
			linetext = linetext + f + ",";
		}
		datawriter.WriteLine (linetext);
		datawriter.Close ();
	}*/
	

	private string GetThumbstickStatus(){
		string status="null";
		/* For general input
		if(OVRInput.Get (OVRInput.Button.Down)) {status="down";}
		if(OVRInput.Get (OVRInput.Button.Up)) {status="up";}
		if(OVRInput.Get (OVRInput.Button.Left)) {status="left";}
		if(OVRInput.Get (OVRInput.Button.Right)) { status="right"; }
		*/

		//For Oculus Touch Controller 
		if(OVRInput.Get (OVRInput.Button.PrimaryThumbstickDown)) { status="L_down"; }
		if(OVRInput.Get (OVRInput.Button.PrimaryThumbstickLeft)) { status="L_left"; }
		if(OVRInput.Get (OVRInput.Button.PrimaryThumbstickRight)) { status="L_right"; }
		if(OVRInput.Get (OVRInput.Button.PrimaryThumbstickUp)) { status="L_up"; }
		if(OVRInput.Get (OVRInput.Button.SecondaryThumbstickDown)) { status="R_down"; }
		if(OVRInput.Get (OVRInput.Button.SecondaryThumbstickLeft)) { status="R_left"; }
		if(OVRInput.Get (OVRInput.Button.SecondaryThumbstickRight)) { status="R_right"; }
		if(OVRInput.Get (OVRInput.Button.SecondaryThumbstickUp)) { status="R_up"; }
		return status;
	}
	private string GetTriggerStatus(){/// Currently both index fingers will trigger the keyword
		string status = "null";
		if (OVRInput.Get (OVRInput.Button.SecondaryIndexTrigger)) {status = "RH_Trigger";}
		return status;
	}
	private string GetGrabberStatus(){/// Currently both index fingers will trigger the keyword
	int l = OVRInput.Get (OVRInput.Button.PrimaryHandTrigger)? 1:0;
	int	r =	OVRInput.Get (OVRInput.Button.SecondaryHandTrigger)? 2:0;	
		/*string status = "null";
	if (OVRInput.Get (OVRInput.Button.PrimaryHandTrigger)) {status = "LH_Grabbing";}
	if (OVRInput.Get (OVRInput.Button.SecondaryHandTrigger)) {status = "RH_Grabbing";}
	if (OVRInput.Get (OVRInput.Button.SecondaryHandTrigger) && OVRInput.Get (OVRInput.Button.PrimaryHandTrigger)) {
		status = "BH_Grabbing";
	}
	return status;*/
	switch(l+r){
			case 0:
				return "null";

			case 1:
				return "LH_Grabb";

			case 2:
				return "RH_Grabb";

			case 3:
				return "BH_Grabb";

			default:
				return "null";
		}
	}

}
