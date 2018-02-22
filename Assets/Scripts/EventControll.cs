using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventControll : MonoBehaviour {

	public bool[] activeSolutions;// Check the visual solution you want to acitve.
	private GameObject[] solutionObjectArray;
	// Use this for initialization


	void Awake () {
		activeVisualSolutions (activeSolutions);
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void activeVisualSolutions(bool[] activearray){
		/*This function catch all the actvie tagged visual soltion and put them into 
		Actvie or Inactvie based on the bool[] array filled in the initialization
		!! All the Tags has to be ACTIVE to be manipulated. */
		for (int i = 0; i < activearray.Length; i++) {
			string tag = string.Format ("Solution_" + i);
			solutionObjectArray = GameObject.FindGameObjectsWithTag (tag);
			if (solutionObjectArray!=null) {
				for (int k = 0; k < solutionObjectArray.Length; k++) {
					solutionObjectArray [k].SetActive (activearray [i]);
					//Debug.Log ("Set " + tag + " to " + activearray [i]);
				}
		
			} else {
				Debug.Log ("The Solution selection array is null");
			}
		}
	}
}
