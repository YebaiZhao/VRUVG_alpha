using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UpdateGameInfo : MonoBehaviour {
	public enum objectType { TextMeshPro = 0, TextMeshProUGUI = 1 };

	public objectType ObjectType;
	private TMP_Text m_text;

	// Use this for initialization
	void Awake () {
		if (ObjectType == 0)
			m_text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
		else
			m_text = GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();

		// Load a new font asset and assign it to the text object.
		m_text.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");
		m_text.fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/Roboto-Bold SDF - Surface");// Load a new material preset which was created with the context menu duplicate.
		m_text.fontSize = 1f;
		m_text.autoSizeTextContainer = true;
		// Set the text
		m_text.text = "A <#0080ff>simple</color> line of text.";
		m_text.enableWordWrapping = false;
		m_text.alignment = TextAlignmentOptions.Left;
	}
	
	// Update is called once per frame
	void Update () {
		m_text.SetText ("Score: "+HiddenGameManager.Instance.playerScore+"        "+
			"Time: " + FormatTimetoPBar (HiddenGameManager.Instance.TimeRemaining));
	}
	private string FormatTimetoPBar(float timeInSeconds){
		int barCount = Mathf.RoundToInt( HiddenGameManager.Instance.TimeRemaining / HiddenGameManager.Instance.maxGameTime*50);
		string bar = new string ('|', barCount);
		string emptybar = new string('|', (50-barCount));

		bar = "<#00ff37>"+bar+"</color>"+"<#dd0000>"+emptybar+"</color>";

		return bar;
	}
	private string FormatTime(float timeInSeconds){
		return string.Format("{0}:{1:00}'", Mathf.FloorToInt(timeInSeconds/60), Mathf.FloorToInt(timeInSeconds % 60));
	}
}
