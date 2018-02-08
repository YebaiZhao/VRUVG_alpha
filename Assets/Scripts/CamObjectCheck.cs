using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamObjectCheck : MonoBehaviour {

	public GameObject anObject;
	public Collider anObjectCollider;
	private Camera cam;
	private Plane[] planes;
	private List <GameObject> objsVG = new List<GameObject>();
	private List <Collider> renderVG = new List<Collider>();
	private bool statusVG = false;
	private bool lastStatus;
	void Start() {
		cam = this.GetComponent<Camera>();
		planes = GeometryUtility.CalculateFrustumPlanes(cam);
		anObjectCollider = anObject.GetComponent<Collider>();
		//Find all objects for visual guidance
		objsVG.AddRange(GameObject.FindGameObjectsWithTag ("Solution_0"));
		objsVG.AddRange(GameObject.FindGameObjectsWithTag ("Solution_1"));
		objsVG.AddRange(GameObject.FindGameObjectsWithTag ("Solution_2"));
		objsVG.AddRange(GameObject.FindGameObjectsWithTag ("Solution_3"));
		objsVG.AddRange(GameObject.FindGameObjectsWithTag ("Solution_4"));
		objsVG.AddRange(GameObject.FindGameObjectsWithTag ("Solution_5"));
		objsVG.AddRange(GameObject.FindGameObjectsWithTag ("Solution_6"));
		foreach (GameObject obj in objsVG) {
			renderVG.AddRange(obj.GetComponentsInChildren<Collider> ());
		}

	}
	void Update() {
		if (renderVG.Count != 0) {
			lastStatus = statusVG;
			/*foreach(Collider r in renderVG){
				if (GeometryUtility.TestPlanesAABB (planes, r.bounds)) {
					statusVG = true;
				} else {
					statusVG = false;
				}
				Debug.Log(Time.realtimeSinceStartup+": "+  r.name + statusVG.ToString());
				if (lastStatus != statusVG) {

				}
					

			}*/


			if (GeometryUtility.TestPlanesAABB (planes,anObjectCollider.bounds)) {
					statusVG = true;
				} else {
					statusVG = false;
				}
			Debug.Log(Time.realtimeSinceStartup+": "+  anObjectCollider.name + statusVG.ToString());
				if (lastStatus != statusVG) {
			}
		}
	}
}







