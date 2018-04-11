using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SessionEnding : MonoBehaviour {
	public enum objectType { TextMeshPro = 0, TextMeshProUGUI = 1 };

	public objectType ObjectType;
	private TMP_Text m_text;
	void OnEnable(){//subscribe event
		HiddenGameManager.DysonClean += EndText;
	}
	void OnDisable(){
		HiddenGameManager.DysonClean -= EndText;
	}
	void Awake () {
		if (ObjectType == 0)
			m_text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
		else
			m_text = GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();
		
		m_text.color = Color.red;
		m_text.alpha = 0;
		GetComponent<Renderer>().enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if(m_text.alpha <1 && GetComponent<Renderer>().enabled){
			m_text.alpha += 0.1f*Time.deltaTime;
		}
	}

	private void EndText(string message){
		m_text.SetText(message);
		GetComponent<Renderer> ().enabled = true;

	}
}
