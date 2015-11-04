using UnityEngine;
using System.Collections;

public class GameMangerScript : MonoBehaviour {
	public GameObject PuzzBlock;
	public GameObject PuzzCursor;

	public float tileW; public float tileH;
	Vector3 StartPos = Vector3.zero;
	int numCol = 6;
	int numRow = 5;

	float PuzzFieldW;
	Vector3 cameraPos;

	void Start(){
		tileW = PuzzBlock.GetComponent<Renderer> ().bounds.size.x;
		tileH = PuzzBlock.GetComponent<Renderer> ().bounds.size.y;

		PuzzFieldW = (numCol - 1) * tileW + 8f;
		cameraPos = new Vector3 (PuzzFieldW / 2, 66f, -10);
		for (int i = 0; i < numRow; i++){
			for (int j = 0; j < numCol; j++){ 
				GameObject newBlock = Instantiate(PuzzBlock);
				newBlock.transform.position = StartPos;
				newBlock.transform.name = "PuzzBlock_"+i+"-"+j;
				StartPos.x += tileW;
			}
			StartPos.x = 0.0f;
			StartPos.y += tileH;
		}

		GameObject GameCursor = (GameObject)Instantiate(PuzzCursor, (Vector2.right * tileW/2), transform.rotation);

		Camera.main.transform.position = cameraPos;
	}

	void Update () {
		//Debug.Log ();
	}
}
