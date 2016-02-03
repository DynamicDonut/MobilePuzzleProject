using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMangerScript : MonoBehaviour {
	public GameObject PuzzBlock;
	public GameObject PuzzCursor;
	public GameObject LeftBlock; public GameObject RightBlock; 
	public GameObject BlockParent;
	public int numOfColors = 6;
	public AnimationCurve BlockMoveSpeed;
    public List<PuzzleBlockScript> BlockList;

    float PuzzAddInterval = 2f; float lastPuzzTime;
    bool stopMove;
	public float tileW; public float tileH;
	Vector3 StartPos = Vector3.zero;
	Vector2 LeftPos; Vector3 RightPos;
	GameObject GameCursor;
	int numCol = 6;
	int numRow = 5;

	public float leftBound, rightBound, topBound, bottomBound;

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

		GameCursor = (GameObject)Instantiate(PuzzCursor, (Vector2.right * tileW/2), transform.rotation);
        GameCursor.name = "Player Cursor";
		Camera.main.transform.position = cameraPos;
	}

	void FixedUpdate () {
        AddBlocks();

		if (Input.GetKeyUp (KeyCode.LeftAlt)) {
		}
	}

	public IEnumerator CurrentSelectedBlocks(GameObject LBlock, GameObject RBlock, float moveTime){
		float timer = 0.0f;
		GameCursor.GetComponent<CursorMovement> ().cursorMove = false;
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
		}

		for (int i = 0; i < BlockParent.transform.childCount; i++) {
			Transform currBlock = BlockParent.transform.GetChild (i);
			if (myPos == currBlock.position - Vector3.left * tileW / 2) {
				LBlock = currBlock.gameObject;
				LeftPos = LBlock.transform.position;
				LBlock.GetComponent<PuzzleBlockScript> ().myCol++;
			}
			if (myPos == currBlock.position - Vector3.right * tileW / 2) {
				RBlock = currBlock.gameObject;
				RightPos = RBlock.transform.position;
				RBlock.GetComponent<PuzzleBlockScript> ().myCol--;
  			}
    	}*/

        LeftPos = LBlock.transform.position;
		RightPos = RBlock.transform.position;

        while (timer <= moveTime) {
            LBlock.transform.position = Vector3.Lerp(LBlock.transform.position, RightPos, BlockMoveSpeed.Evaluate(timer / moveTime));
            RBlock.transform.position = Vector3.Lerp(RBlock.transform.position, LeftPos, BlockMoveSpeed.Evaluate(timer / moveTime));
            timer += Time.deltaTime;
            if (LBlock.transform.position.x > RightPos.x - 0.01f && RBlock.transform.position.x > LeftPos.x - 0.01f) {
                LBlock.transform.position = RightPos;
                RBlock.transform.position = LeftPos;
            }
            yield return null;
        }
        BlockParent.BroadcastMessage("CheckAdjBlocks", Vector2.left);
        BlockParent.BroadcastMessage("CheckAdjBlocks", Vector2.up);
        BlockParent.BroadcastMessage("CheckAdjBlocks", Vector2.right);
        BlockParent.BroadcastMessage("CheckAdjBlocks", Vector2.down);
        GameCursor.GetComponent<CursorMovement> ().cursorMove = true;
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
                BlockList.Add(newBlock.GetComponent<PuzzleBlockScript>());

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

    void AddBlocks() {
        GameObject bottomBlock = GameObject.Find("1,1");
        GameObject myCursor = GameObject.Find("Player Cursor");
        if (!stopMove) {
            if (Time.time > lastPuzzTime + PuzzAddInterval) {
                foreach (PuzzleBlockScript pBlock in BlockList) {
                    pBlock.transform.position = new Vector3(pBlock.transform.position.x, pBlock.transform.position.y + 0.4f, pBlock.transform.position.z);
                }
                myCursor.transform.position = new Vector3(myCursor.transform.position.x, myCursor.transform.position.y + 0.4f, myCursor.transform.position.z);
                lastPuzzTime = Time.time;
            }
        }
    }
}
