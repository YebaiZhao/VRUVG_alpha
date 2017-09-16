//Yebai Zhao 9/14/17
//Manage the status of the cat

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yb_catScript : MonoBehaviour {
	public float Blend;
	[SerializeField] private float StateMachineChangeTime = 7.0f;
	private float timeNextPeriod = 0f;
	private Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		anim = GetComponent<Animator>();
		if (Time.realtimeSinceStartup > timeNextPeriod) {
			timeNextPeriod += StateMachineChangeTime;
			anim.SetFloat("Blend", Random.Range(0f ,3f));

		}


		if (GameManager.Instance.buttonStatus == "Index_Triggered") {
			anim.SetBool ("makeMeow", true);
		}
		else{
			anim.SetBool ("makeMeow", false);
		}


	}
}
