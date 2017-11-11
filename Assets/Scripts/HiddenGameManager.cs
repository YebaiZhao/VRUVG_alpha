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

public class HiddenGameManager : Singleton<HiddenGameManager> {
	//Time
	private float _timeRemaining;
	public float maxGameTime = 5 * 60; // In seconds.
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
	public float catHideTime = 5f;
	public float catBrithTime = 0f;
	public float catDeathTime = 0f;
	public float uiReportTime = 0f;
	private GameObject[] inCatTags;
	private CatMovment catScript;

	//UI
	public bool degreeRestirt = true;

	void Start () {
		TimeRemaining = maxGameTime;
		inCatTags = GameObject.FindGameObjectsWithTag ("Unique");////It cant catch inactive objs, so put in Start()

	
		catScript = inCatTags[0].GetComponent<CatMovment> ();


	}

	// Update is called once per frame
	void Update () {
		TimeRemaining -= Time.deltaTime;
		thumbstickStatus = GetThumbstickStatus();
		buttonStatus = GetButtonStatus ();
		catRebirth ();

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
	public void catRebirth(){
		if ((Time.realtimeSinceStartup - catDeathTime)>catHideTime && catHide == true) {
			catHide = false;
			foreach (GameObject obj in inCatTags) {
				obj.SetActive (true);
			}
			catScript.objectMove ();
		}

	}





	public void ReactionTimer(){
		uiReportTime = catDeathTime - catBrithTime;
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