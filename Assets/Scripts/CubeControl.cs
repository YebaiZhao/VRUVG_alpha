using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour {
	private float repostTill = 0f;
	[SerializeField] float repositionT = 5f;
	private Vector3 repostAt = new Vector3(41.26f, 5.7f, 36.1f);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(Time.realtimeSinceStartup - repostTill) < 0.5f) {
			transform.position=(repostAt);
			repostTill = 0f;
		}
	}
	public void CubeWakeUp(){
		repostTill = Time.realtimeSinceStartup + repositionT;
		Debug.Log (repostTill+" "+Time.realtimeSinceStartup);
		repostAt += new Vector3 (Random.Range (-0.1f, 0.1f), 0f, Random.Range (-0.3f, 0.3f));

	}


}
