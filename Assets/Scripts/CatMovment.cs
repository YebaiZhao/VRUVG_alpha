//Yebai Zhao
//This script is used to move the cat in a sequence way
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovment : MonoBehaviour {


	[SerializeField] float telePeriod = 20.0f;
	[SerializeField] private Transform lookTarget ;
//	private float startingY;
	private float teleNextPeriod = 0;
	private List<Vector3> moveList = new List<Vector3>();
	private int moveListPointer = 0;
	// Use this for initialization
	void Start () {
		//Move list
		moveList.Add (new Vector3(48.5f, 3.86f, 35.54f));
		moveList.Add (new Vector3(34.95f, 3.86f, 36.38f));
		moveList.Add (new Vector3(37.72f, 2.25f, 22.1f));
		moveList.Add (new Vector3(50.92f, 3.26f, 30.22f));
		//
	}

	// Update is called once per frame
	void Update () {
		ArrowMove ();
		//transform.LookAt(lookTarget); //look towards the player
	}



	public void ArrowMove(){
		if (Time.realtimeSinceStartup > teleNextPeriod) {
			teleNextPeriod += telePeriod;
			transform.position = (moveList[moveListPointer]);
			if (moveListPointer+1 == moveList.Count) {//moving the pointer
				moveListPointer = 0;
			} else {
				moveListPointer++;
			}
		}
	}
}
