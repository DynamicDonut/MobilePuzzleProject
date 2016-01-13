using UnityEngine;
using System.Collections;

public class CursorMovement : MonoBehaviour {
	int MovementType = 0; //0 = KB, 1 = Mouse, 2 = Gamepad, 3 = Touch/Mobile
	GameMangerScript MainScript;
	GameObject LeftSelectedBlock, RightSelectedBlock;

	// Use this for initialization
	void Start () {
		MainScript = GameObject.Find ("GameManager").GetComponent<GameMangerScript> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (MovementType == 0) {
			if (Input.GetKeyUp(KeyCode.Space)){
				StartCoroutine(MainScript.CurrentSelectedBlocks(transform.position, 0.5f));
			} 

			if (Input.GetKeyUp(KeyCode.LeftArrow)){
				if (transform.position.x > MainScript.leftBound + MainScript.tileW/2){
					transform.position = transform.position + Vector3.left * MainScript.tileW;
				}
			} else if (Input.GetKeyUp(KeyCode.RightArrow)){
				if (transform.position.x < MainScript.rightBound - MainScript.tileW/2){
					transform.position = transform.position + Vector3.right * MainScript.tileW;
				}
			} else if (Input.GetKeyUp(KeyCode.UpArrow)){
				if (transform.position.y < MainScript.topBound){
					transform.position = transform.position + Vector3.up * MainScript.tileH;
				}
			} else if (Input.GetKeyUp(KeyCode.DownArrow)){
				if (transform.position.y > MainScript.bottomBound){
					transform.position = transform.position + Vector3.down * MainScript.tileH;
				}
			}
		} else if (MovementType == 1) {
		} else if (MovementType == 2) {
		} else if (MovementType == 3) {
		}

		//Debug.Log (transform.position);
	}

	void SelectedBlocks(Vector3 myPos){
		for (int i = 0; i < MainScript.BlockParent.transform.childCount; i++) {
			Transform currBlock = MainScript.BlockParent.transform.GetChild(i);
			if (myPos == currBlock.position - Vector3.left * MainScript.tileW/2){
				currBlock.GetComponent<PuzzleBlockScript>().myCol++;
				LeftSelectedBlock = currBlock.gameObject;
			}
			if (myPos == currBlock.position - Vector3.right * MainScript.tileW/2){
				currBlock.GetComponent<PuzzleBlockScript>().myCol--;
				RightSelectedBlock = currBlock.gameObject;
			}
			/*
			if (PuzzCursorPos == currBlock.position - Vector3.right * tileW/2){
				RightBlock = currBlock.gameObject;
				RightPos = RightBlock.transform.position;
				RightBlock.GetComponent<PuzzleBlockScript>().myCol--;
			}
			*/
		}
	}
}
