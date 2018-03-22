using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	/*By Yebai
	This script can change the VG before and during the game.
	But REMBEMBER:
		The solution list now only support 7 items. You should change the code to fit more.
		You should not change the VG when VG is active.

	 */
public class VG_Controll : MonoBehaviour {

	private List<GameObject[]> solutionObjectList;// Check the visual solution you want to acitve.
	public enum solutionToActive { PushArrow = 0, OffScreenIndi = 1, Flash = 2, StaticArrow = 3, PullArrow = 4, HandArrow = 5, WeartherCockArrow = 6 };
	// Use this for initialization
	public solutionToActive activeOne;
	private int listLength = 7;
	void Start () {
		solutionObjectList = new List <GameObject[]>();
		for (int i = 0; i < listLength; i++) {
			string tag = string.Format ("Solution_" + i);
			solutionObjectList.Add(GameObject.FindGameObjectsWithTag (tag));
			}

		if (solutionObjectList!=null)
        {
            setVGList((int)activeOne);
        }
    }

	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha0)){
			setVGList(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1)){
			setVGList(1);
		}else if(Input.GetKeyDown(KeyCode.Alpha2)){
			setVGList(2);
		}else if(Input.GetKeyDown(KeyCode.Alpha3)){
			setVGList(3);
		}else if(Input.GetKeyDown(KeyCode.Alpha4)){
			setVGList(4);
		}else if(Input.GetKeyDown(KeyCode.Alpha5)){
			setVGList(5);
		}else if(Input.GetKeyDown(KeyCode.Alpha6)){
			setVGList(6);
		}

	}

	private void setVGList(int i){
        foreach (GameObject[] objA in solutionObjectList) {
            foreach(GameObject obj in objA){
				obj.SetActive(false);
			}
        }
		foreach(GameObject obj in solutionObjectList[i]){
			obj.SetActive(true);
		}
		HiddenGameManager.dataArray[30] = i.ToString();
		Debug.Log("Setting active: "+i);
    }
}


