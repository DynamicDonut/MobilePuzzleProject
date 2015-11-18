using UnityEngine;
using System.Collections;

public class PuzzleBlockScript : MonoBehaviour {
	public Sprite[] PuzzBlockColors;
	public int BlockColorInt;
	public int myRow; public int myCol;
	GameObject myParent;
	SpriteRenderer mySprite;

	// Use this for initialization
	void Start () {
		myParent = transform.gameObject;
		mySprite = myParent.GetComponent<SpriteRenderer>();
		mySprite.sprite = PuzzBlockColors [BlockColorInt];
	}
	
	// Update is called once per frame
	void Update () {
		mySprite.sprite = PuzzBlockColors [BlockColorInt];
		this.name = myRow + "," + myCol;
	}
}
