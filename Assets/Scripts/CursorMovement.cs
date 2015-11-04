using UnityEngine;
using System.Collections;

public class CursorMovement : MonoBehaviour {
	int MovementType = 0; //0 = KB, 1 = Mouse, 2 = Gamepad, 3 = Touch/Mobile
	GameMangerScript MainScript;

	// Use this for initialization
	void Start () {
		MainScript = GameObject.Find ("GameManager").GetComponent<GameMangerScript> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (MovementType == 0) {
			if (Input.GetKeyUp(KeyCode.LeftArrow)){
				transform.position = transform.position + Vector3.left * MainScript.tileW;
			} else if (Input.GetKeyUp(KeyCode.RightArrow)){
				transform.position = transform.position + Vector3.right * MainScript.tileW;
			} else if (Input.GetKeyUp(KeyCode.UpArrow)){
				transform.position = transform.position + Vector3.up * MainScript.tileH;
			} else if (Input.GetKeyUp(KeyCode.DownArrow)){
				transform.position = transform.position + Vector3.down * MainScript.tileH;
			}

			if (Input.GetKeyUp(KeyCode.Space)){

			}
		} else if (MovementType == 1) {
		} else if (MovementType == 2) {
		} else if (MovementType == 3) {
		}
	}
}
