//Yebai Zhao 9/14/17
//Manage the status of the cat

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateControl : MonoBehaviour {
	public float Blend;
	[SerializeField] private float StateMachineChangeTime = 7.0f;
	private float tpNextPeriod = 0f;
	private Animator anim;

	// Use this for initialization
	void Start () {
		//anim.SetFloat("Blend", 0f);
	}
	
	// Update is called once per frame
	void Update () {
		anim = GetComponent<Animator>();
		if (Time.realtimeSinceStartup > tpNextPeriod) { //When it's time to relocate the cat
			tpNextPeriod = StateMachineChangeTime + Time.realtimeSinceStartup;
			anim.SetFloat("Blend", Random.Range(0f ,3f));
		}


		if (HiddenGameManager.Instance.buttonStatus == "Index_Triggered" && HiddenGameManager.Instance.catHide == true) {
			anim.SetBool ("makeMeow", true);
			//Set a time delay here!!!!!!!!!!!!!!!!!!!
			SkinnedMeshRenderer render = GameObject.FindWithTag("Respawn").GetComponentInChildren<SkinnedMeshRenderer>();
			render.enabled = false;
		}
		else{
			anim.SetBool ("makeMeow", false);
		}



	}
}
