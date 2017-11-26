using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UpdateInstrBoard : MonoBehaviour {
	public enum objectType { TextMeshPro = 0, TextMeshProUGUI = 1 };

	public objectType ObjectType;
	public bool isStatic = false;
	private TMP_Text m_text;
	[SerializeField] string[] colorOptions = {"<#0080ff>red</color>","<#bf3434>yellow</color>","<#e5df08>white</color>"};
	[SerializeField] float TextChange = 8f;
	private float nextPeriod = 0f;

	void Awake()
	{
		// Get a reference to the TMP text component if one already exists otherwise add one.
		// This example show the convenience of having both TMP components derive from TMP_Text. 
		if (ObjectType == 0)
			m_text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
		else
			m_text = GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();

		// Load a new font asset and assign it to the text object.
		m_text.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/Roboto-Bold SDF");
		m_text.fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/Roboto-Bold SDF - Surface");// Load a new material preset which was created with the context menu duplicate.
		m_text.fontSize = 1.5f;

		// Set the text
		m_text.text = "A <#0080ff>simple</color> line of text.";
		m_text.enableWordWrapping = false;
		m_text.alignment = TextAlignmentOptions.Center;
	}


	void Update()
	{
		if (Time.realtimeSinceStartup > nextPeriod ){
			m_text.SetText("Put the "+colorOptions[Random.Range(0,colorOptions.Length)]+" cube in the box.");
			nextPeriod += TextChange;
		}
	}
}
