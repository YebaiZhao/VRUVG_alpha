using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventControll : MonoBehaviour {
	public Transform weaponObject;
	public AudioClip gunclip;
	public AudioSource audioSource1;
	public bool[] activeSolutions;// Check the visual solution you want to acitve.
	private GameObject[] solutionObjectArray;
	// Use this for initialization


	void Start () {
		audioSource1 = GetComponent<AudioSource> ();
		audioSource1.clip = gunclip;
		activeVisualSolutions (activeSolutions);
	}
	
	// Update is called once per frame
	void Update () {
		LaserFunction ();

	}



	private void LaserFunction(){
		Ray debug = new Ray (weaponObject.position, weaponObject.forward);

		if (HiddenGameManager.Instance.buttonStatus == "Index_Triggered") {
			audioSource1.Play (); ///play the gun shot

			RaycastHit hit;
			if (Physics.Raycast(weaponObject.position, weaponObject.forward, out hit)){
				if(hit.collider.gameObject.CompareTag("Unique")){
					hit.collider.gameObject.SetActive (false);

					//Destroy(hit.collider.gameObject); //kill the cat
					HiddenGameManager.Instance.catDeathTime= Time.realtimeSinceStartup;
					HiddenGameManager.Instance.ReactionTimer ();
					HiddenGameManager.Instance.catHide = true;

					Debug.Log("Cat Deactived by laser");

				}
			}
		}
	}


	private void activeVisualSolutions(bool[] activearray){
		/*This function catch all the actvie tagged visual soltion and put them into 
		Actvie or Inactvie based on the bool[] array filled in the initialization
		!! All the Tags has to be ACTIVE to be manipulated. */
		for (int i = 0; i < activearray.Length; i++) {
			string tag = string.Format ("Solution_" + i);
			solutionObjectArray = GameObject.FindGameObjectsWithTag (tag);
			if (solutionObjectArray!=null) {
				for (int k = 0; k < solutionObjectArray.Length; k++) {
					solutionObjectArray [k].SetActive (activearray [i]);
					Debug.Log ("Set " + tag + " to " + activearray [i]);
				}
		
			} else {
				Debug.Log ("The Solution selection array is null");
			}
		}
	}
}
