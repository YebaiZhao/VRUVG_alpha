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
	public float maxGameTime = 180; //seconds.
	public float TimeRemaining//the time remaining from the begining of the game in sec.
	{
		get { return _timeRemaining; }
		set { _timeRemaining = value; }
	}

	//Thumb
	public string thumbstickStatus = "null";
	public string buttonStatus = "null";
	//Cat
	//Is the cat regenalbe
	public bool catHide = false;// if the laser in controller hit the cat
	public float catHideTime = 10f;
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
	private float textChangeT = 8f;
	public float nextTextChangeTime = 0f;
	public string currentColor = "";
	public int playerScore = 0;
	public bool changeText = true;
	public string scoredCube="null";
	public Vector3 cubeHome = new Vector3(41.29f, 4.52f, 35.9f);


	//2ndTask
	public List<float> reportTimeList = new List<float>();
	public float total_UGTime = 0f;
	public float mean_UGTime = 0f;


	void Start () {

		TimeRemaining = maxGameTime;
		inCatTags = GameObject.FindGameObjectsWithTag ("Unique");////It cant catch inactive objs, so put in Start()
		catScript = inCatTags[0].GetComponent<CatMovment> ();
		catScript.CatTeleport (9);
		WriteLineToTXT (" ");
		WriteLineToTXT ("A New Game");

	}

	// Update is called once per frame
	void Update () {
		TimeRemaining -= Time.deltaTime;
		thumbstickStatus = GetThumbstickStatus();
		buttonStatus = GetButtonStatus ();

		if ((Time.realtimeSinceStartup - catDeathTime) > catHideTime && catHide == true) {
			catRebirth ();
		}

		MainTaskMananger ();


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
		catBirthTime = Time.realtimeSinceStartup;
	}



	public void CountReactionTime(){
	
		uiReportTime = catDeathTime - catBirthTime;
		reportTimeList.Add (uiReportTime);
		WriteLineToTXT (string.Format("Shoot: "+reportTimeList.Count +" || Reaction time: "+uiReportTime));
	}



	public void MainTaskMananger(){

		if (Time.realtimeSinceStartup >nextTextChangeTime) { //update the text every 8sec
			changeText = true;
			nextTextChangeTime += textChangeT;
		}
		if (scoredCube != "null") { //once scored cube info is received


			if (scoredCube == currentColor) {//correct
				playerScore += 100;
				changeText = true;
			} else { //incorrect 
				playerScore -= 200;
				changeText = true;
			}
			nextTextChangeTime = Time.realtimeSinceStartup + textChangeT;
			scoredCube = "null";
		}
	}

	public void CalulateReaction(){
		total_UGTime = 0f;
		foreach (float t in reportTimeList) {
			total_UGTime += t;
		}
		mean_UGTime = total_UGTime / reportTimeList.Count;
	}

	public static void WriteLineToTXT(string text){
		string destination = Application.persistentDataPath + "/save.txt";
		//FileStream file;
		//if(File.Exists(destination)) file = File.OpenWrite(destination);
		//else file = File.Create(destination);
		StreamWriter writer = new StreamWriter (destination, true);
		writer.WriteLine (text);
		writer.Close ();
	}

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

	private string GetButtonStatus(){/// Currently both index fingers will trigger the keyword
		string status = "null";
		if (OVRInput.Get (OVRInput.Button.PrimaryIndexTrigger)) {status = "Index_Triggered";}
		if (OVRInput.Get (OVRInput.Button.SecondaryIndexTrigger)) {status = "Index_Triggered";}
		return status;
	}


}
