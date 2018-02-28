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
	//Time
	private float _timeRemaining;
	public float maxGameTime = 300; //seconds.
	public float TimeRemaining//the time remaining from the begining of the game in sec.
	{
		get { return _timeRemaining; }
		set { _timeRemaining = value; }
	}

	//Data Writer
	public static string[] dataArray = {"Timestamp","HeadX","HeadY","HeadZ","LHandX","LHandY","LHandZ","RHandX","RHandY","RHandZ",
		"HeadtoBoard","HeadtoCat","Score","CatLoc","CatEvent","LHTrigger","LRTrigger","LHGrabb","RHGrabb", "TColor", 
		"Text","Cube","Bonus","CatX" ,"CatY" ,"CatZ"};
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
	//Is the cat regenalbe
	public bool catHide = false;// if the laser in controller hit the cat
	public float catHideTime = 15f;
	public float catTeleportTime = 0f;
	public float catBirthTime =0f;
	public float catDeathTime = 0f;
	public float uiReportTime = 0f;
	public Vector3 catLocation ;
	private GameObject[] inCatTags;
	private CatMovment catScript;

	//UI
	public bool holdVG = false;



	//Maintask
	public int playerScore = 0;
	public Vector3 cubeHome = new Vector3(41.29f, 4.52f, 35.9f);


	//2ndTask
	public List<float> reportTimeList = new List<float>();
	public float total_UGTime = 0f;
	public float mean_UGTime = 0f;



	void Start () {
		destination = Application.persistentDataPath +"/"+System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
		LogToCSV ();
		dataArray [13] = "";
		dataArray [23] = "";
		dataArray [24] = "";
		dataArray [25] = "";
		TimeRemaining = maxGameTime;
		inCatTags = GameObject.FindGameObjectsWithTag ("Unique");////It cant catch inactive objs, so put in Start()
		head = GameObject.FindGameObjectWithTag ("MainCamera");
		lefthand = GameObject.FindGameObjectWithTag ("lefthand");
		righthand = GameObject.FindGameObjectWithTag ("righthand");
		boardposition =new Vector3 (40.856f, 4.716515f, 36.1156f);

		catScript = inCatTags[0].GetComponent<CatMovment> ();
		//catScript.CatTeleport (9);//Cat start everytime at 9
		inCatTags[0].SetActive( false);
		catHide = true;

	}

	// Update is called once per frame
	void Update () {
		TimeRemaining -= Time.deltaTime;

		//thumbstickStatus = GetThumbstickStatus(); - obseloete


		if ((Time.realtimeSinceStartup - catDeathTime) > catHideTime+Random.Range(0,10) && catHide == true) {
			catRebirth ();
		}

		if (Time.realtimeSinceStartup > nextCSVtime) {
			LogData ();
			LogToCSV ();
			nextCSVtime += CSVperiod;
		}
			

		if(TimeRemaining<=0 || Input.GetKey("escape")){ // quit game under these circumstances
			Application.Quit();
		}



	}


	/// <End of the Update>
	/// //////////////////////////////////////////////////////////////////////
	/// </summary>
	/// <returns>The thumbstick status.</returns>
	/// 
	/// 
	/// 
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



	public void catRebirth(){
		catHide = false;
		foreach (GameObject obj in inCatTags) {
			obj.SetActive (true);
		}
		//catScript.CatOnDesk ();
		catScript.CatRamdonTP();
		catBirthTime = Time.realtimeSinceStartup;
		LogEvent (13, 14, "WaitForTP", "CatRebrith");
	}



	public void CountReactionTime(){
		uiReportTime = catDeathTime - catBirthTime;
		reportTimeList.Add (uiReportTime);
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
		dataArray [0] = Time.realtimeSinceStartup.ToString();
		dataArray [1] = head.transform.position.x.ToString();
		dataArray [2] = head.transform.position.y.ToString();
		dataArray [3] = head.transform.position.z.ToString();
		dataArray [4] = lefthand.transform.position.x.ToString();
		dataArray [5] = lefthand.transform.position.y.ToString();
		dataArray [6] = lefthand.transform.position.z.ToString();
		dataArray [7] = righthand.transform.position.x.ToString();
		dataArray [8] = righthand.transform.position.y.ToString();
		dataArray [9] = righthand.transform.position.z.ToString();
		headToBorad = boardposition - head.transform.position;
		headToCat = catLocation - head.transform.position;
		dataArray [10] = Vector3.Angle (head.transform.forward, headToBorad).ToString();
		dataArray [11] = Vector3.Angle (head.transform.forward, headToCat).ToString();
		dataArray [12] = playerScore.ToString();
		dataArray [14] = "";
		dataArray [15] = OVRInput.Get (OVRInput.Button.PrimaryIndexTrigger)? "true":"";
		dataArray [16] = OVRInput.Get (OVRInput.Button.SecondaryIndexTrigger)? "true":"";
		dataArray [17] = OVRInput.Get (OVRInput.Button.PrimaryHandTrigger)? "true":"";
		dataArray [18] = OVRInput.Get (OVRInput.Button.SecondaryHandTrigger)? "true":"";
		dataArray [21] = "";
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
