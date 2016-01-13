using UnityEngine;
using System.Collections;

public class GameMangerScript : MonoBehaviour {
	public GameObject PuzzBlock;
	public GameObject PuzzCursor;
	public GameObject LeftBlock; public GameObject RightBlock; 
	public GameObject BlockParent;
	public int numOfColors = 6;
	public AnimationCurve BlockMoveSpeed;
	//bool startingClear = true;

	public float tileW; public float tileH;
	Vector3 StartPos = Vector3.zero;
	Vector2 LeftPos; Vector3 RightPos;
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
		PuzzleBlockSpawning ();
		//CheckStartingBlocks ();

		topBound = bottomBound + tileH * (numRow + 4);
		GameObject GameCursor = (GameObject)Instantiate(PuzzCursor, (Vector2.right * tileW/2), transform.rotation);
		Camera.main.transform.position = cameraPos;
	}

	void FixedUpdate () {
		if (Input.GetKeyUp (KeyCode.LeftAlt)) {
		}
	}

	public IEnumerator CurrentSelectedBlocks(Vector3 myPos, float moveTime){
		float timer = 0.0f;
		/*for (int i = 0; i < BlockParent.transform.childCount; i++) {
			Transform currBlock = BlockParent.transform.GetChild(i);
			if (PuzzCursorPos == currBlock.position - Vector3.left * tileW/2){
				LeftBlock = currBlock.gameObject;
				LeftPos = LeftBlock.transform.position;
				LeftBlock.GetComponent<PuzzleBlockScript>().myCol++;
			}
			if (PuzzCursorPos == currBlock.position - Vector3.right * tileW/2){
				RightBlock = currBlock.gameObject;
				RightPos = RightBlock.transform.position;
				RightBlock.GetComponent<PuzzleBlockScript>().myCol--;
			}
		}*/

		for (int i = 0; i < BlockParent.transform.childCount; i++) {
			Transform currBlock = BlockParent.transform.GetChild (i);
			if (myPos == currBlock.position - Vector3.left * tileW / 2) {
				LeftBlock = currBlock.gameObject;
				LeftPos = LeftBlock.transform.position;
				LeftBlock.GetComponent<PuzzleBlockScript> ().myCol++;
			}
			if (myPos == currBlock.position - Vector3.right * tileW / 2) {
				RightBlock = currBlock.gameObject;
				RightPos = RightBlock.transform.position;
				RightBlock.GetComponent<PuzzleBlockScript> ().myCol--;
			}
		}

		//LeftBlock.transform.position = Vector3.MoveTowards (LeftBlock.transform.position, RightPos, tileW);
		//RightBlock.transform.position = Vector3.MoveTowards (RightBlock.transform.position, LeftPos, tileW);

		while (timer <= moveTime) {
			LeftBlock.transform.position = Vector3.Lerp (LeftBlock.transform.position, RightPos, BlockMoveSpeed.Evaluate(timer/moveTime));
			RightBlock.transform.position = Vector3.Lerp (RightBlock.transform.position, LeftPos, BlockMoveSpeed.Evaluate(timer/moveTime));
			timer += Time.deltaTime;
			yield return null;
		}
	}

	void PuzzleBlockSpawning(){
		for (int i = 1; i < numRow+1; i++){
			for (int j = 1; j < numCol+1; j++){ 
				GameObject newBlock = Instantiate(PuzzBlock);
				newBlock.GetComponent<PuzzleBlockScript>().BlockColorInt = Random.Range(0,numOfColors);
				newBlock.GetComponent<PuzzleBlockScript>().myRow = i;
				newBlock.GetComponent<PuzzleBlockScript>().myCol = j;
				newBlock.transform.position = StartPos;
				newBlock.transform.name = i+","+j;
				newBlock.transform.parent = BlockParent.transform;
				if (i==1 && j==1){
					leftBound = newBlock.transform.position.x;
					bottomBound = newBlock.transform.position.y;
				} else if (i==1 && j==numCol){
					rightBound = newBlock.transform.position.x;
				}

				if (i>=2){
					CheckBlocks(newBlock, "bottom", i, j);
				}

				if (j>=2){
					CheckBlocks(newBlock, "left", i, j);
				}
				StartPos.x += tileW;
			}
			StartPos.x = 0.0f;
			StartPos.y += tileH;
		}
	}

	void CheckBlocks(GameObject nBlock, string dir, int i, int j){
		int a = 0; // matches with i 
		int b = 0; // matches with j
		if (dir == "left"){
			b = -1;
		} else if (dir == "right"){
			b = 1;
		} else if (dir == "top"){
			a = 1;
		} else if(dir == "bottom"){
			a = -1;
		}


		GameObject currItem = GameObject.Find((i+a) + "," + (j+b));
		if (nBlock.GetComponent<PuzzleBlockScript>().BlockColorInt == currItem.GetComponent<PuzzleBlockScript>().BlockColorInt){
			nBlock.GetComponent<PuzzleBlockScript>().BlockColorInt = Random.Range(0,numOfColors);
			CheckBlocks(nBlock, dir, i, j);
		}
	}

	void ClearBlocks(){

	}
}
