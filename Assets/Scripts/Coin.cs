using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	[SerializeField]
	private float rotateSpeed = 1.0f;
	private float floatSpeed = 0.5f;
	public Transform camera;

	// Use this for initialization
	void Start () {
		//transform.Rotate (transform.up, Random.Range (0f, 360f));
		StartCoroutine (Spin ());
	}
	
	private IEnumerator Spin(){
		while(true){
			//transform.Rotate (Vector3.up, 360 * rotateSpeed * Time.deltaTime);
			transform.LookAt(camera);
			yield return 0;
		}
	}
}