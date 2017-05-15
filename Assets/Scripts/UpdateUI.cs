/*For changing a text labe on a canvas*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour {

	[SerializeField]
	private Text timerlabel ;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timerlabel.text = FormatTime (GameManager.Instance.TimeRemaining);
	}
	private string FormatTime(float timeInSeconds){
		return string.Format("{0}:{1:00}", Mathf.FloorToInt(timeInSeconds/60), Mathf.FloorToInt(timeInSeconds % 60));
	}
}
