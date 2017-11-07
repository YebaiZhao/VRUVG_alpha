/*For changing a text labe on a canvas*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHandUI : MonoBehaviour {

	[SerializeField] private Text timerlabel ;
	[SerializeField] private Text controllerlabel;
	[SerializeField] private Text fpslabel;
	[SerializeField] private Text dataA;
	[SerializeField] private Text dataB;
	[SerializeField] float fpsMeasurePeriod = 2.0f;

	private float m_FpsNextPeriod = 0;
	// Use this for initialization
	void Start () {
		m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;//for telling FPS
	}

	// Update is called once per frame
	void Update () {
		timerlabel.text = FormatTime (GameManager.Instance.TimeRemaining);//time to the end of the game
		controllerlabel.text = GameManager.Instance.buttonStatus;//button
		dataA.text = FormatTime(GameManager.Instance.uiReportTime);//cat reaction time

		if (Time.realtimeSinceStartup > m_FpsNextPeriod){//fps
			m_FpsNextPeriod += fpsMeasurePeriod;
			fpslabel.text = string.Format("{0:F1} FPS", OVRPlugin.GetAppFramerate());
		}
	}





	private string FormatTime(float timeInSeconds){
		return string.Format("{0}:{1:00}", Mathf.FloorToInt(timeInSeconds/60), Mathf.FloorToInt(timeInSeconds % 60));
	}


}
