/*Managing game data such as time */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
	private float _timeRemaining;
	public float maxGameTime = 1 * 60; // In seconds.


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
	}
}