using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatTypeText : MonoBehaviour {
	public enum objectType { TextMeshPro = 0, TextMeshProUGUI = 1 };

	public objectType ObjectType;
	private TMP_Text m_text;
	private float flytime = 0;
	void OnEnable(){//subscribe event
		LaserControll.LaserHitCat += CatEpitaph;
	}
	void OnDisable(){
		LaserControll.LaserHitCat -= CatEpitaph;
	}
	void Awake () {
		if (ObjectType == 0)
			m_text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
		else
			m_text = GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();

		// Load a new font asset and assign it to the text object.
		/*m_text.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");
		m_text.fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/Roboto-Bold SDF - Surface");// Load a new material preset which was created with the context menu duplicate.
		m_text.fontSize = 10f;
		m_text.autoSizeTextContainer = true;
		// Set the text
		m_text.text = "A <#0080ff>simple</color> line of text.";
		m_text.enableWordWrapping = false;
		m_text.alignment = TextAlignmentOptions.Left;*/
	}

	// Update is called once per frame
	void Update () {
		if (Time.realtimeSinceStartup < flytime) {
			transform.Translate (Vector3.up * 2 * Time.deltaTime);
		}else m_text.SetText("");
	}

	private void CatEpitaph(){
		transform.position = HiddenGameManager.Instance.catLocation;
		transform.LookAt (Camera.main.transform.position);
		transform.Rotate (Vector3.up, 180f);
		m_text.SetText (HiddenGameManager.dataArray [26]);
		flytime = Time.realtimeSinceStartup + 5f;
	}
}
