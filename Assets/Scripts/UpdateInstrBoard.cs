using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;



public class UpdateInstrBoard : MonoBehaviour {
	public enum objectType { TextMeshPro = 0, TextMeshProUGUI = 1 };

	public objectType ObjectType;
	private TMP_Text m_text;
	[SerializeField] string[] visualColor = {"2374B8","00B000","9818E5","E73D3D","E7E727"};
	[SerializeField] string[] colorText = {"blue cube","green cube","purple cube","red cube","yellow cube"}; 
	[SerializeField] int[] bonusArray = {-30, 20, -10, 10, 0, 10, 10, 20, 30, 50, 80, 130};//Fibonacci numbers F-4 to F7
	//[SerializeField] int[] bonusCatArray = {130, -80, 50, -30, 20, -10, 10, 0, 10, 10};//Fibonacci numbers F-7 to F2
	[SerializeField] float bonusPeriod = 20f;
	private int colorPointer;
	private int textPointer;
	private int bonus;
	private float nextChangeBonus;
	private string countdown = "xxxxx";
	private int countIn10;
	public AudioClip correct;
	public AudioClip wrong;
	private AudioSource source;

	void OnEnable(){//subscribe event
		BoxDetection.OnCubeEnter += ChangeColorText;
		HiddenGameManager.DysonClean += Hide;
	}
	void OnDisable(){
		BoxDetection.OnCubeEnter -= ChangeColorText;
		HiddenGameManager.DysonClean -= Hide;
	}


	void Awake()
	{
		/*parentCanvas = GetComponentInParent<Canvas> ();
		RectTransform rt = parentCanvas.transform.GetComponent<RectTransform> ();
		m_text.rectTransform.sizeDelta = new Vector2 (rt.sizeDelta.x * rt.localScale.x, rt.sizeDelta.y * rt.localScale.y);*/
		// Get a reference to the TMP text component if one already exists otherwise add one.
		// This example show the convenience of having both TMP components derive from TMP_Text. 
		source = GetComponent<AudioSource>();

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
		m_text.text = "A simple line of text.";
		m_text.enableWordWrapping = false;
		m_text.alignment = TextAlignmentOptions.Left;

	}
	void Start(){
		nextChangeBonus = 0f;
		ChangeColorText ("null");
	}


	void Update(){
		m_text.SetText("Find a    <size=150%><#" + visualColor[colorPointer] + ">"+ colorText[textPointer]+"</color></size>\n" +
			"Bonus    <size=150%>" + bonus+"</size>   "+countdown);
		
		countIn10 =Mathf.CeilToInt ((nextChangeBonus - Time.realtimeSinceStartup) / bonusPeriod * 10f);
		ChangeCountDown (countIn10);
	}

	private void ChangeCountDown(int i){
		switch (i) {
		case 10:
			countdown = "==========";
			break;
		case 9:
			countdown = "=========";
			break;
		case 8:
			countdown = "========";
			break;
		case 7:
			countdown = "=======";
			break;
		case 6:
			countdown = "======";
			break;
		case 5:
			countdown = "=====";
			break;
		case 4:
			countdown = "====";
			break;
		case 3:
			countdown = "===";
			break;
		case 2:
			countdown = "==";
			break;
		case 1:
			countdown = "=";
			break;
		case 0:
			ChangeBonus ();
			break;
		default:
			ChangeBonus ();
			break;
		}
	}


	private void ChangeBonus(){
		/*if (HiddenGameManager.Instance.catHide) {
			bonus = bonusArray [Random.Range (0, bonusArray.Length)];
			nextChangeBonus += bonusPeriod;
		} else {
			bonus = bonusCatArray [Random.Range (0, bonusCatArray.Length)];
			nextChangeBonus += bonusPeriod;
		}*/
		bonus = bonusArray [Random.Range (0, bonusArray.Length)];
		nextChangeBonus += bonusPeriod;
		HiddenGameManager.dataArray[22] = bonus.ToString();
	}

	private void ChangeColorText(string scoredCube){

		if (scoredCube != "null") { //once scored cube info is received
			int oldScore = HiddenGameManager.Instance.playerScore;
			if (scoredCube == colorText [textPointer]) {//correct
				HiddenGameManager.Instance.playerScore += bonus;
			} else { //incorrect 
				HiddenGameManager.Instance.playerScore -= bonus;
			}

			HiddenGameManager.Instance.LogEvent (21, scoredCube);

			if (HiddenGameManager.Instance.playerScore < oldScore){source.PlayOneShot (wrong);}
			else {source.PlayOneShot (correct);}
			//nextTextChangeTime = Time.realtimeSinceStartup + textChangeT;
		}

		colorPointer = Random.Range (0, visualColor.Length);
		textPointer = (colorPointer + Random.Range (1, colorText.Length)) % colorText.Length;

		HiddenGameManager.dataArray[20] = colorText [textPointer].Remove(colorText [textPointer].Length -5);
		HiddenGameManager.dataArray[19] = colorText [colorPointer].Remove(colorText [colorPointer].Length -5);
	}
	private void Hide(string message){
		gameObject.SetActive (false);
	}
}
