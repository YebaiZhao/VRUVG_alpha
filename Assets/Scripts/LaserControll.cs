﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControll : MonoBehaviour {
	[SerializeField] float laserminRange = 3f;
	public Transform weaponObject;
	public AudioClip gunclip;
	public AudioSource audioSource1;
	private RaycastHit hit;
	private ParticleSystem laserparticle;
	public delegate void LaserCatEvent();
	public static event LaserCatEvent LaserHitCat;

	// Use this for initialization
	void Start () {
		audioSource1 = GetComponent<AudioSource> ();
		audioSource1.clip = gunclip;
		laserparticle = GetComponentInChildren<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update () {
		LaserFunction ();

	}
	private void LaserFunction(){
		//Ray debug = new Ray (weaponObject.position, weaponObject.forward);
		if (Physics.Raycast (weaponObject.position, weaponObject.forward, out hit)) {//constantly shooting raycast
			
			if (HiddenGameManager.dataArray[15]=="true" ||HiddenGameManager.dataArray[16]=="true") {
				audioSource1.Play (); ///play the gun shot
				if (hit.collider.gameObject.CompareTag ("Unique")) {		// if this the cat
					hit.collider.gameObject.SetActive (false);
					laserparticle.Stop ();
					if (LaserHitCat != null) {
						LaserHitCat ();
					}

				}//else doing nothing

			}

			if (!HiddenGameManager.Instance.catHide && !HiddenGameManager.Instance.holdVG) { // if you shold show VG
				if (Vector3.Distance (weaponObject.position, hit.point) > laserminRange) {//if it's far
					laserparticle.transform.position = hit.point;
					if (laserparticle.isStopped) { 
						laserparticle.Play ();
					} 
				} else {
					laserparticle.Stop ();//if is't not far
				}
	
			} else {
				laserparticle.Stop ();
			}

		}

	}
}
