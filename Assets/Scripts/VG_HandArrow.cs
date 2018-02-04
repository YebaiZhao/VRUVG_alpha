using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VG_HandArrow : MonoBehaviour {
	[SerializeField] Transform target;
	private Renderer rd;
	// Use this for initialization
	void Start () {
		rd = GetComponentInChildren<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (target);


		if (HiddenGameManager.Instance.catHide || HiddenGameManager.Instance.holdVG) {
			rd.enabled = false;		//hide the arrow
		} else {
			rd.enabled = true;		//show the arrow
		}
	}
}