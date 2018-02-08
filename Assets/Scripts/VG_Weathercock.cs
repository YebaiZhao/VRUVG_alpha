using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VG_Weathercock : MonoBehaviour {
	//public float angleDelta;
	//[SerializeField] private Transform m_DesiredLocation;      // Indicates which direction the player should be facing (uses world space forward if null).
	//[SerializeField] private Transform m_Camera; 
	[SerializeField] private Transform DesiredLocation;
	private Renderer[] arrowRenders;
	private GameObject[] LookAtObjects;
	// Use this for initialization
	void Start () {
		//transform.eulerAngles = new Vector3 (0, m_Camera.eulerAngles.y, 0);
		arrowRenders = GetComponentsInChildren<Renderer> ();
		LookAtObjects = GameObject.FindGameObjectsWithTag ("LookAt");
	}
	// Update is called once per frame
	void Update () {
		//transform.position = m_Camera.position;
		//Vector3 targetOnXZ = new Vector3 (m_DesiredLocation.position.x, this.transform.position.y, m_DesiredLocation.position.z);
		//transform.LookAt(targetOnXZ);// Refresh both posistion and rotation.

		if (HiddenGameManager.Instance.catHide || HiddenGameManager.Instance.holdVG) {
			foreach (Renderer r in arrowRenders) {
				r.enabled = false;
			}

		} else {
			foreach (Renderer r in arrowRenders) {
				r.enabled = true;
			}
			foreach (GameObject obj in LookAtObjects){
				obj.transform.LookAt (DesiredLocation);
			}
		}
		
	}
}
