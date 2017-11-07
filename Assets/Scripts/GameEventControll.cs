using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventControll : MonoBehaviour {
	public Transform weaponObject;
	public AudioClip gunclip;
	public AudioSource audioSource1;
	// Use this for initialization
	void Start () {
		audioSource1 = GetComponent<AudioSource> ();
		audioSource1.clip = gunclip;
	}
	
	// Update is called once per frame
	void Update () {
		LaserFunction ();
	}




	private void LaserFunction(){
		Ray debug = new Ray (weaponObject.position, weaponObject.forward);

		if (GameManager.Instance.buttonStatus == "Index_Triggered") {
			audioSource1.Play (); ///play the gun shot

			RaycastHit hit;
			if (Physics.Raycast(weaponObject.position, weaponObject.forward, out hit)){
				if(hit.collider.gameObject.CompareTag("Cat")){
					Destroy(hit.collider.gameObject); //kill the cat
					GameManager.Instance.catDeathTime= Time.realtimeSinceStartup;
					GameManager.Instance.CatTimer ();
				}
			}
		}
	}
}
