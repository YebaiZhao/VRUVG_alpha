using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour {
	[SerializeField] private float floatSpeed = 1.0f; // In cycles (up and down) per second
	[SerializeField] private float movementDistance = 0.03f; // The maximum distance the coin can move up and down
	[SerializeField] float roatePeriod = 5.0f;
	[SerializeField] private Transform lookTarget ;
	private float startingY;
	private bool isMovingUp = true;
	private float roateNextPeriod = 0;
	private ArrayList moveList = new ArrayList (4);
	private int moveListPointer = 0;
	// Use this for initialization
	void Start () {
		startingY = transform.position.y;
		moveList.Add (Vector3.left);
		moveList.Add (Vector3.back);
		moveList.Add (Vector3.forward);
		moveList.Add (Vector3.right);
	}
	
	// Update is called once per frame
	void Update () {
		Float ();
		//ArrowMove ();
		ArrowRoate ();
		//transform.LookAt(lookTarget); //look towards the player
	}
	private void Float()
	{
		float newY = transform.position.y + (isMovingUp ? 1 : -1) * 2 * movementDistance * floatSpeed * Time.deltaTime;

		if (newY > startingY + movementDistance){
			newY = startingY + movementDistance;
			isMovingUp = false;
		}
		else if (newY < startingY){
			newY = startingY;
			isMovingUp = true;
		}

		transform.position = new Vector3(transform.position.x, newY, transform.position.z);

	}

	public void ArrowRoate(){
		if (Time.realtimeSinceStartup > roateNextPeriod) {
			roateNextPeriod += roatePeriod;
			transform.LookAt(lookTarget);
			transform.Rotate (Vector3.forward*90*Mathf.Round(Random.Range(0.5f,3.5f)), Space.Self);

		}

	}
	public void ArrowMove(){
		//if (Time.realtimeSinceStartup > roateNextPeriod) {
		//	transform.Translate (moveList[moveListPointer], Space.Self);
		//	if (moveListPointer < 3) {
		//		moveListPointer++;
		//	} else {
		//		moveListPointer = 0;
		//	}
		//}
	}
}
