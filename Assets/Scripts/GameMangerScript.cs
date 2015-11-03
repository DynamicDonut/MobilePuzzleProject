using UnityEngine;
using System.Collections;

public class GameMangerScript : MonoBehaviour {
	public GameObject PuzzBlock;
	Vector3 StartPos = Vector3.zero;
	int numCol = 6;
	int numRow = 5;

	float PuzzFieldW;
	Vector3 cameraPos;

	void Start(){
		PuzzFieldW = (numCol - 1) * 16f + 8f;
		cameraPos = new Vector3 (PuzzFieldW / 2, 66f, -10);
		for (int i = 0; i < numRow; i++){
			for (int j = 0; j < numCol; j++){ 
				GameObject newBlock = Instantiate(PuzzBlock);
				newBlock.transform.position = StartPos;
				newBlock.transform.name = "PuzzBlock_"+i+"-"+j;
				StartPos.x += 16f;
			}
			StartPos.x = 0.0f;
			StartPos.y += 16f;
		}
		Camera.main.transform.position = cameraPos;
	}

	void Update () {
		
	}
}
