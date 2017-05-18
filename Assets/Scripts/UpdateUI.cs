/*For changing a text labe on a canvas*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour {

	[SerializeField] private Text timerlabel ;
	[SerializeField] private Text thumbsticklabel;
	[SerializeField] private Text fpslabel;
	[SerializeField] float fpsMeasurePeriod = 2.0f;

	private float m_FpsNextPeriod = 0;
	// Use this for initialization
	void Start () {
		m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
	}

	// Update is called once per frame
	void Update () {
		timerlabel.text = FormatTime (GameManager.Instance.TimeRemaining);
		thumbsticklabel.text = GameManager.Instance.thumbstickStatus;

		if (Time.realtimeSinceStartup > m_FpsNextPeriod){
			m_FpsNextPeriod += fpsMeasurePeriod;
			fpslabel.text = string.Format("{0:F1} FPS", OVRPlugin.GetAppFramerate());
		}
	}





	private string FormatTime(float timeInSeconds){
		return string.Format("{0}:{1:00}", Mathf.FloorToInt(timeInSeconds/60), Mathf.FloorToInt(timeInSeconds % 60));
	}


}
