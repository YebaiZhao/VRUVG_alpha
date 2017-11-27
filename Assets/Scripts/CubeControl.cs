using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour {
	private float repostTill = 0f;
	[SerializeField] float repositionT = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs( Time.realtimeSinceStartup - repostTill) < 0.5f) {
			transform.position=(new Vector3 (41f, 5.7f, 36.3f));
			repostTill = 0f;
		}
	}
	public void CubeWakeUp(){
		repostTill = Time.realtimeSinceStartup + repositionT;
		Debug.Log (repostTill+" "+Time.realtimeSinceStartup);

	}


}
