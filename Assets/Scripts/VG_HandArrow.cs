using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VG_HandArrow : MonoBehaviour {
	[SerializeField] Transform target;
	[SerializeField] float MaxDis = 15f;
	[SerializeField] float MinDis = 0.5f;
	[SerializeField] float MaxScaleZ = 6f;
	[SerializeField] float MinScaleZ = 0.5f;
	private float b;
	private float k;
	private Renderer rd;
	// Use this for initialization
	void Start () {
		rd = GetComponentInChildren<Renderer> ();
		b = (MinDis * MaxScaleZ - MaxDis * MinScaleZ) / (MinDis - MaxDis);
		k = (MaxScaleZ - MinScaleZ) / (MaxDis - MinDis);
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (target);
		transform.localScale = new Vector3 (1, 1, ((target.position-this.transform.position).magnitude*k +b));

		if (HiddenGameManager.Instance.catHide || HiddenGameManager.Instance.holdVG) {
			rd.enabled = false;		//hide the arrow
		} else {
			rd.enabled = true;		//show the arrow
		}
	}
}