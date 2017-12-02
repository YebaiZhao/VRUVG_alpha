using System.Collections;
using UnityEngine;

public class VG_Flash : MonoBehaviour {

	private float flashInterval = 1.0f;
	private Light lt;
	//private Material[] mat;
	[SerializeField]  bool Active = true;
	// Use this for initialization
	void Start() {
		lt = GetComponent<Light>();
		//mat = GetComponent<Renderer>().materials;
		//Debug.Log (mat.Length);
		//mat[0].EnableKeyword ("_EMISSION");

	}
	void Update() {
		if(!HiddenGameManager.Instance.holdVG && !HiddenGameManager.Instance.catHide){
			float phi = Time.time / flashInterval * 2 * Mathf.PI;
			float amplitude = Mathf.Cos(phi) * 2f + 0.5F;
			lt.intensity = amplitude;
			//mat[0].SetColor("_EmissionColor", Color.Lerp(Color.red, Color.clear, phi));
		}
	}
}