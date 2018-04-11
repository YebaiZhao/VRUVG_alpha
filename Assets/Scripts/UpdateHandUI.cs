/*For changing a text labe on a canvas*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHandUI : MonoBehaviour {

	[SerializeField] private Text timerlabel = null;
	[SerializeField] private Text dataC= null;
	[SerializeField] private Text fpslabel= null;
	[SerializeField] private Text dataA= null;
	[SerializeField] private Text dataB= null;
	[SerializeField] float fpsMeasurePeriod = 2.0f;

	private float m_FpsNextPeriod = 0;
	// Use this for initialization
	void Start () {
		m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;//for telling FPS
	}

	// Update is called once per frame
	void Update () {
		//timerlabel.text = FormatTime (HiddenGameManager.Instance.TimeRemaining);//time to the end of the game
		//dataC.text = HiddenGameManager.Instance.buttonStatus;//button
		dataA.text = FormatTime(HiddenGameManager.Instance.uiReportTime);//cat reaction time


		if (Time.realtimeSinceStartup > m_FpsNextPeriod){//fps
			m_FpsNextPeriod += fpsMeasurePeriod;
			fpslabel.text = string.Format("{0:F1} FPS", OVRPlugin.GetAppFramerate());


			HiddenGameManager.Instance.CalulateReaction ();
			string textB = "MRT: " + HiddenGameManager.Instance.mean_UGTime;
			dataB.text = textB;
		}
	}


	private string FormatTime(float timeInSeconds){
		return string.Format("{0}:{1:00}'{2:0000}", Mathf.FloorToInt(timeInSeconds/60), Mathf.FloorToInt(timeInSeconds % 60), Mathf.FloorToInt((timeInSeconds % 1)*1000));
	}


}
