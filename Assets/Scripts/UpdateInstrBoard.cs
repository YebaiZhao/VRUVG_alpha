using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;



public class UpdateInstrBoard : MonoBehaviour {
	public enum objectType { TextMeshPro = 0, TextMeshProUGUI = 1 };

	public objectType ObjectType;
	private TMP_Text m_text;
	[SerializeField] string[] visualColor;
	[SerializeField] string[] colorText; 

	void Awake()
	{
		/*parentCanvas = GetComponentInParent<Canvas> ();
		RectTransform rt = parentCanvas.transform.GetComponent<RectTransform> ();
		m_text.rectTransform.sizeDelta = new Vector2 (rt.sizeDelta.x * rt.localScale.x, rt.sizeDelta.y * rt.localScale.y);*/
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
		m_text.autoSizeTextContainer = true;
		// Set the text
		m_text.text = "A <#0080ff>simple</color> line of text.";
		m_text.enableWordWrapping = false;
		m_text.alignment = TextAlignmentOptions.Left;
	}


	void Update()
	{
		if (HiddenGameManager.Instance.changeText ){ //reroll
			int pointer = Random.Range (0, Mathf.Min(visualColor.Length, colorText.Length));
			HiddenGameManager.Instance.currentColor = colorText [pointer];
			m_text.SetText("Put the <#" + visualColor[pointer] + ">"+ colorText[pointer]+"</color> in the wood box. \n"
				+"Your Score is: "+HiddenGameManager.Instance.playerScore);
			HiddenGameManager.Instance.changeText = false;
		}

	}

	public void UpdateText(){
		int pointer = Random.Range (0, Mathf.Min(visualColor.Length, colorText.Length));
		HiddenGameManager.Instance.currentColor = colorText [pointer];
		m_text.SetText("Put the <#" + visualColor[pointer] + ">"+ colorText[pointer]+"</color> in the wood box. \n"
			+"Your Score is: "+HiddenGameManager.Instance.playerScore);
		HiddenGameManager.Instance.changeText = false;
	}


}
