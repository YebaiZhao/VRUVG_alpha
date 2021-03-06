﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour {
	private float teleNextPeriod = 0f;
	[SerializeField] float telePeriod = 5f;
	private Vector3 myhome;
	private bool imInTheBox = false;
	// Use this for initialization




	void OnEnable(){//subscribe event
		HiddenGameManager.DysonClean += HideMyself;
	}
	void OnDisable(){
		HiddenGameManager.DysonClean -= HideMyself;
	}


	void Start () {
		myhome = HiddenGameManager.Instance.cubeHome;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.realtimeSinceStartup >teleNextPeriod){
			//do nothing at home
			if (!CheckRange (myhome)) {//if not at home
				if (HiddenGameManager.Instance.catHide ) {//but cat is hiding
					GoHome ();
				}
				else if (imInTheBox && (HiddenGameManager.dataArray[26]!="CubeTaker")){
					GoHome ();
				}
				else if (imInTheBox && (HiddenGameManager.dataArray[26]=="CubeTaker")) {
						GoToCat ();
				}
				
			}

			teleNextPeriod += telePeriod;
		}


	}
	public void CubeWakeUp(){ //wake up after in the box
		teleNextPeriod = Time.realtimeSinceStartup + telePeriod;
		imInTheBox = true;

	}
	public bool CheckRange(Vector3 center){
		float d = Vector3.Distance (transform.position, center);
		if (d < 0.4f) {
			return true;
		} else {
			return false;
		}
	}

	public void GoHome(){
		transform.position = (myhome + new Vector3 (Random.Range (-0.1f, 0.1f), 0.3f, Random.Range (-0.3f, 0.3f)));
		imInTheBox = false;
	}

	public void GoToCat(){
		transform.position = (HiddenGameManager.Instance.catLocation + new Vector3 (Random.Range (-0.3f, 0.3f), 0.3f, Random.Range (-0.3f, 0.3f)));
	}
	private void HideMyself(string message){
		GetComponent<Renderer> ().enabled = false;
	}
}
