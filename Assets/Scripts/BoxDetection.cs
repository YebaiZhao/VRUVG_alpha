using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetection : MonoBehaviour {

	private CubeControl cubecontrol;


	void OnTriggerEnter(Collider cube){
		HiddenGameManager.Instance.scoredCube = cube.gameObject.tag; //pass the tag to gamemanager
		cubecontrol = cube.GetComponent<CubeControl>();
		if (cubecontrol != null) {
			cubecontrol.CubeWakeUp ();
		}

	}

	// Update is called once per frame

}
