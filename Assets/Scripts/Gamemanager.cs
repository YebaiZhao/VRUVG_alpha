/*Managing game data such as time */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
	private float _timeRemaining;
	public float maxGameTime = 1 * 60; // In seconds.
	public string thumbstickStatus = "null";

	public float TimeRemaining //the time remaining from the begining of the game in sec.
	{
		get { return _timeRemaining; }
		set { _timeRemaining = value; }
	}
		
	// Use this for initialization
	void Start () {
		TimeRemaining = maxGameTime;
	}
	
	// Update is called once per frame
	void Update () {
		TimeRemaining -= Time.deltaTime;
		if(TimeRemaining<=0 || Input.GetKey("escape")){ // quit game under these circumstances
			Application.Quit();
		}
		thumbstickStatus = GetThumbstickStatus();
	}

	private string GetThumbstickStatus(){
		string status="null";
		/* For general input
		if(OVRInput.Get (OVRInput.Button.Down)) {status="down";};
		if(OVRInput.Get (OVRInput.Button.Up)) {status="up";};		
		if(OVRInput.Get (OVRInput.Button.Left)) {status="left";};
		if(OVRInput.Get (OVRInput.Button.Right)) { status="right"; };
		*/

		//For Oculus Touch Controller 
		if(OVRInput.Get (OVRInput.Button.PrimaryThumbstickDown)) { status="L_down"; };
		if(OVRInput.Get (OVRInput.Button.PrimaryThumbstickLeft)) { status="L_left"; };
		if(OVRInput.Get (OVRInput.Button.PrimaryThumbstickRight)) { status="L_right"; };
		if(OVRInput.Get (OVRInput.Button.PrimaryThumbstickUp)) { status="L_up"; };
		if(OVRInput.Get (OVRInput.Button.SecondaryThumbstickDown)) { status="R_down"; };
		if(OVRInput.Get (OVRInput.Button.SecondaryThumbstickLeft)) { status="R_left"; };
		if(OVRInput.Get (OVRInput.Button.SecondaryThumbstickRight)) { status="R_right"; };
		if(OVRInput.Get (OVRInput.Button.SecondaryThumbstickUp)) { status="R_up"; };
		return status;
	}
		
}