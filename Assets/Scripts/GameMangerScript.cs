using UnityEngine;
using System.Collections;

public class GameMangerScript : MonoBehaviour {
	public GameObject PuzzBlock;
	public GameObject PuzzCursor;
	public GameObject LeftBlock; public GameObject RightBlock;
	GameObject BlockParent;

	public float tileW; public float tileH;
	Vector3 StartPos = Vector3.zero;
	int numCol = 6;
	int numRow = 5;

	public float leftBound; public float rightBound; public float topBound; public float bottomBound;

	float PuzzFieldW;
	Vector3 cameraPos;

	void Start(){
		tileW = PuzzBlock.GetComponent<Renderer> ().bounds.size.x;
		tileH = PuzzBlock.GetComponent<Renderer> ().bounds.size.y;

		PuzzFieldW = (numCol - 1) * tileW + 8f;
		cameraPos = new Vector3 (PuzzFieldW / 2, 66f, -10);
		BlockParent = new GameObject ("Puzzle Blocks");

		//Puzzle Block Spawning
		for (int i = 1; i < numRow+1; i++){
			for (int j = 1; j < numCol+1; j++){ 
				GameObject newBlock = Instantiate(PuzzBlock);
				newBlock.transform.position = StartPos;
				newBlock.transform.name = i+","+j;
				newBlock.transform.parent = BlockParent.transform;
				if (newBlock.transform.name == "1,1"){
					leftBound = newBlock.transform.position.x;
					bottomBound = newBlock.transform.position.y;
				} else if (newBlock.transform.name == "1," + (numCol)){
					rightBound = newBlock.transform.position.x;
				}
				StartPos.x += tileW;
			}
			StartPos.x = 0.0f;
			StartPos.y += tileH;
		}

		topBound = bottomBound + tileH * (numRow + 4);
		GameObject GameCursor = (GameObject)Instantiate(PuzzCursor, (Vector2.right * tileW/2), transform.rotation);
		Camera.main.transform.position = cameraPos;
	}

	void Update () {
	}

	 public void CurrentSelectedBlocks(Vector3 PuzzCursorPos){
		Vector3 LeftPos = Vector3.zero; 
		Vector3 RightPos = Vector3.zero;
		//string BothBlocks = "";
		for (int i = 0; i < BlockParent.transform.childCount; i++) {
			Transform currBlock = BlockParent.transform.GetChild(i);
			if (PuzzCursorPos == currBlock.position - Vector3.left * tileW/2){
				LeftBlock = currBlock.gameObject;
				LeftPos = LeftBlock.transform.position;
			}
			if (PuzzCursorPos == currBlock.position - Vector3.right * tileW/2){
				RightBlock = currBlock.gameObject;
				RightPos = RightBlock.transform.position;
			}
		}
		LeftBlock.transform.position = Vector3.MoveTowards (LeftBlock.transform.position, RightPos, tileW * Time.time);
		RightBlock.transform.position = Vector3.MoveTowards (RightBlock.transform.position, LeftPos, tileW * Time.time);
		Debug.Log ("It ran");
	}
}
