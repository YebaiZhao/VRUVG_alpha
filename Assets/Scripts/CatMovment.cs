//Yebai Zhao
//This script is used to move the cat in a sequence way
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovment : MonoBehaviour {


	[SerializeField] float Period = 20.0f; //how long does the cat teleport again
	[SerializeField] private Transform lookTarget ;
	public AudioClip aclip;
	public AudioSource audioSource1;

//	private float startingY;
	private float teleNextPeriod = -0.3f;
	private List<Vector3> moveList = new List<Vector3>();
	private List<Vector3> rotateList = new List<Vector3>();
	// Use this for initialization
	void Start () {


		audioSource1 = GetComponent<AudioSource> ();
		audioSource1.clip = aclip;
		//Move list of 15
		moveList.Add (new Vector3(48.5f, 3.695f, 35.54f));//0
		moveList.Add (new Vector3(42.101f, 5.513f, 44.098f));
		moveList.Add (new Vector3(38.49f, 3.846f, 32.68f));
		moveList.Add (new Vector3(32.96f, 3.17f, 31.18f));
		moveList.Add (new Vector3(38.49f, 3.795f, 39.866f));
		moveList.Add (new Vector3(35.7071f, 2.412f, 25.066f));
		moveList.Add (new Vector3(49.2f, 3.921f, 42.334f));
		moveList.Add (new Vector3(45.156f, 3.068f, 28.307f));
		moveList.Add (new Vector3(47.575f, 7.074f, 41.151f));
		moveList.Add (new Vector3(41.11f, 3.557f, 38.7f));//9
		moveList.Add (new Vector3(37.6925f, 2.953f, 28.131f));
		moveList.Add (new Vector3(49.89f, 3.278f, 30.57f));
		moveList.Add (new Vector3(45.32f, 3.55f, 34.21f));
		moveList.Add (new Vector3(45.32f, 3.754f, 39.49f));
		moveList.Add (new Vector3(37.52f, 4.333f, 44.184f));//14
		//Rotate list of 15
		rotateList.Add(new Vector3(-4.028f, 52.091f, 0.132f));//0
		rotateList.Add(new Vector3(-16.744f, 202.13f, -9.543f));
		rotateList.Add(new Vector3(1.232f, 137.2361f, -5.031f));
		rotateList.Add(new Vector3(2.058f, 202.917f, 1.814f));
		rotateList.Add(new Vector3(10.131f, 180f, 1.814f));
		rotateList.Add(new Vector3(-4.743f, 78.9f, -9.911f));
		rotateList.Add(new Vector3(-1.433f, 375.844f, -0.357f));
		rotateList.Add(new Vector3(1.415f, 254.198f, 6.169f));
		rotateList.Add(new Vector3(-25.956f, 73.764f, 18.737f));
		rotateList.Add(new Vector3(3.302f, 180.777f, -0.676f));//9
		rotateList.Add(new Vector3(-4.743f, 135.223f, -9.911f));
		rotateList.Add(new Vector3(-4.743f, 61.859f, -9.911f));
		rotateList.Add(new Vector3(1.169f, -46.541f, -0.96f));
		rotateList.Add(new Vector3(1.169f, -46.541f, -0.96f));
		rotateList.Add(new Vector3(1.169f, -131.026f, -0.96f));
	}

	// Update is called once per frame
	void Update () {
		//if (Time.realtimeSinceStartup > teleNextPeriod) { //Re tp the cat 
		//	CatTeleport ();
		//}
		//if (Time.time < teleNextPeriod && HiddenGameManager.Instance.holdVG) { //Let the cat sneak through the desk
		//	transform.Translate ((Vector3.forward) * 2f * Time.deltaTime);
		//}
		//transform.LookAt(lookTarget); //look towards the player
	}



	public void CatTeleport(){
		int p =Random.Range (0, Mathf.Min(moveList.Count, rotateList.Count));
		transform.position = moveList[p];
		HiddenGameManager.Instance.catLocation = moveList [p];
		transform.eulerAngles = rotateList [p];
		HiddenGameManager.Instance.catTeleportTime = Time.realtimeSinceStartup;//Tell the GM that the cat has relocated
		HiddenGameManager.Instance.holdVG = false;
		HiddenGameManager.dataArray [23] = moveList [p].x.ToString();
		HiddenGameManager.dataArray [24] = moveList [p].y.ToString ();
		HiddenGameManager.dataArray [25] = moveList [p].z.ToString ();
		HiddenGameManager.Instance.LogEvent (13, 14, p.ToString(), "CatTP");

	}


	/*public void CatOnDesk(){
		transform.position = new Vector3(40.483f, 3.47f, 35.698f);
		transform.eulerAngles = new Vector3 (0f, 180f, 0f);
		audioSource1.Play();

		teleNextPeriod = Time.realtimeSinceStartup + 1f;
		HiddenGameManager.Instance.holdVG = true;
	}*/

	public void CatRamdonTP(){
		teleNextPeriod = Time.realtimeSinceStartup;
	}

}
