using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour {
	[SerializeField]
	private float flashInterval = 1.0f;
	private bool turnActive = true;
	// Use this for initialization
	void Start () {
		transform.Rotate (0, 90*Random.Range (0, 3),0);
		StartCoroutine (Change90());
	}

	private IEnumerator Change90(){
		while(turnActive){
			transform.Rotate(transform.forward, 90);
			yield return new WaitForSeconds(flashInterval);
		}
	}

	// Update is called once per frame
	//void Update () {
		
	//}
}
