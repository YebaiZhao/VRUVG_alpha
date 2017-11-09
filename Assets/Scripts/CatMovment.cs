//Yebai Zhao
//This script is used to move the cat in a sequence way
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovment : MonoBehaviour {


	[SerializeField] float telePeriod = 20.0f;
	public float catRebirthTime = 5.0f;
	[SerializeField] private Transform lookTarget ;
//	private float startingY;
	private float teleNextPeriod = 0;
	private List<Vector3> moveList = new List<Vector3>();
	private List<Vector3> rotateList = new List<Vector3>();
	// Use this for initialization
	void Start () {
		//Move list of 10
		moveList.Add (new Vector3(48.5f, 3.695f, 35.54f));//1
		moveList.Add (new Vector3(42.101f, 5.513f, 44.098f));
		moveList.Add (new Vector3(37.76f, 3.823f, 34.47f));
		moveList.Add (new Vector3(31.29f, 3.459f, 36.02f));
		moveList.Add (new Vector3(23.32f, 1.961f, 19.63f));
		moveList.Add (new Vector3(48.44f, 2.576f, 25.44f));
		moveList.Add (new Vector3(55.818f, 3.921f, 42.334f));
		moveList.Add (new Vector3(45.156f, 3.068f, 28.307f));
		moveList.Add (new Vector3(47.575f, 7.074f, 41.151f));
		moveList.Add (new Vector3(41.11f, 3.557f, 38.7f));//10
		//Rotate list of 10
		rotateList.Add(new Vector3(-4.028f, 52.091f, 0.132f));//1
		rotateList.Add(new Vector3(-16.744f, 202.13f, -9.543f));
		rotateList.Add(new Vector3(1.232f, 137.2361f, -5.031f));
		rotateList.Add(new Vector3(2.058f, 202.917f, 1.814f));
		rotateList.Add(new Vector3(2.058f, 110.072f, 1.814f));
		rotateList.Add(new Vector3(9.702f, 214.264f, 4.641f));
		rotateList.Add(new Vector3(-1.433f, 375.844f, -0.357f));
		rotateList.Add(new Vector3(1.415f, 254.198f, 6.169f));
		rotateList.Add(new Vector3(-25.956f, 73.764f, 18.737f));
		rotateList.Add(new Vector3(3.302f, 180.777f, -0.676f));//10
	}

	// Update is called once per frame
	void Update () {
		objectMove ();
		//transform.LookAt(lookTarget); //look towards the player
	}



	public void objectMove(){
		if (Time.realtimeSinceStartup > teleNextPeriod) {
			teleNextPeriod += telePeriod;
			int p = Random.Range (0, Mathf.Min(moveList.Count, rotateList.Count));
			Debug.Log ("moving the cat to the " + p + "location");
			transform.position = moveList[p];
			transform.eulerAngles = rotateList [p];
			HiddenGameManager.Instance.catBrithTime = Time.realtimeSinceStartup;
		}
	}




}
