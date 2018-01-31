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
		m_text.fontSize = 1f;
		m_text.autoSizeTextContainer = true;
		// Set the text
		m_text.text = "A <#0080ff>simple</color> line of text.";
		m_text.enableWordWrapping = false;
		m_text.alignment = TextAlignmentOptions.Left;
	}


	void Update()
	{
		if (HiddenGameManager.Instance.changeText ){ //reroll
			UpdateText();
		}

	}

	public void UpdateText(){
		int colorPointer = Random.Range (0, visualColor.Length);
		int textPointer = (colorPointer + Random.Range (1, colorText.Length)) % colorText.Length;

		HiddenGameManager.Instance.currentColor = colorText [textPointer];
		m_text.SetText("Put a <#" + visualColor[colorPointer] + ">"+ colorText[textPointer]+"</color> in the wood box. \n"
			+"Your Score is: "+HiddenGameManager.Instance.playerScore);
		HiddenGameManager.Instance.changeText = false;
	}

}
