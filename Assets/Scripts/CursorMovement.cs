using UnityEngine;
using System.Collections;

public class CursorMovement : MonoBehaviour {
	enum mType {KB, Mouse, Gamepad, Mobile}; //Keyboard, Mouse, Gamepad, Touch/Mobile

    mType controlStyle;
	GameMangerScript MainScript;
	public bool cursorMove = true;
	public GameObject LeftSelectedBlock, RightSelectedBlock;

	// Use this for initialization
	void Start () {
		MainScript = GameObject.Find ("GameManager").GetComponent<GameMangerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		SelectedBlocks(this.transform.position);
		if (cursorMove) {
			if (controlStyle == mType.KB) {
				if (Input.GetKeyUp (KeyCode.Space)) {
					StartCoroutine (MainScript.CurrentSelectedBlocks (LeftSelectedBlock, RightSelectedBlock, 0.25f));
                }

                if (Input.GetKeyUp (KeyCode.LeftArrow)) {
					if (transform.position.x > MainScript.leftBound + MainScript.tileW / 2) {
						transform.position = transform.position + Vector3.left * MainScript.tileW;
					}
				} else if (Input.GetKeyUp (KeyCode.RightArrow)) {
					if (transform.position.x < MainScript.rightBound - MainScript.tileW / 2) {
						transform.position = transform.position + Vector3.right * MainScript.tileW;
					}
				} else if (Input.GetKeyUp (KeyCode.UpArrow)) {
					if (transform.position.y < MainScript.topBound) {
						transform.position = transform.position + Vector3.up * MainScript.tileH;
					}
				} else if (Input.GetKeyUp (KeyCode.DownArrow)) {
					if (transform.position.y > MainScript.bottomBound) {
						transform.position = transform.position + Vector3.down * MainScript.tileH;
					}
				}
			} else if (controlStyle == mType.Mouse) {
			} else if (controlStyle == mType.Gamepad) {
			} else if (controlStyle == mType.Mobile) {
			}
		}
		//Debug.Log (transform.position);
	}

	void SelectedBlocks(Vector3 myPos){
//		for (int i = 0; i < MainScript.BlockParent.transform.childCount; i++) {
//			Transform currBlock = MainScript.BlockParent.transform.GetChild(i);
//			if (currBlock.position == myPos + Vector3.left * MainScript.tileW/2){
//				//currBlock.GetComponent<PuzzleBlockScript>().myCol++;
//				LeftSelectedBlock = currBlock.gameObject;
//			}
//			if (currBlock.position == myPos + Vector3.right * MainScript.tileW/2){
//				//currBlock.GetComponent<PuzzleBlockScript>().myCol--;
//				RightSelectedBlock = currBlock.gameObject;
//			}
//		}

		LeftSelectedBlock = Physics2D.OverlapPoint (myPos + (Vector3.left * MainScript.tileW / 2)).gameObject;
		RightSelectedBlock = Physics2D.OverlapPoint (myPos + (Vector3.right * MainScript.tileW / 2)).gameObject;
	}
}
