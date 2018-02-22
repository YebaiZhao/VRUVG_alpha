using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetection : MonoBehaviour {

	public delegate void CubeEvent(string tag);
	public static event CubeEvent OnCubeEnter;
	private CubeControl cubecontrol;


	void OnTriggerEnter(Collider cube){ //If a cube enters
		 
		//HiddenGameManager.Instance.scoredCube = cube.gameObject.tag; //pass the tag to gamemanager
		cubecontrol = cube.GetComponent<CubeControl>();
		if (cubecontrol != null) {
			cubecontrol.CubeWakeUp ();
		}

		if (OnCubeEnter != null) {
			OnCubeEnter (cube.gameObject.tag.ToString());
		}
	}

	// Update is called once per frame

}
