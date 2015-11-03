using UnityEngine;
using System.Collections;

public class PuzzleBlockScript : MonoBehaviour {
	public Sprite[] PuzzBlockColors;
	GameObject myParent;
	SpriteRenderer mySprite;

	// Use this for initialization
	void Start () {
		myParent = transform.gameObject;
		mySprite = myParent.GetComponent<SpriteRenderer>();

		mySprite.sprite = PuzzBlockColors [Random.Range (0, 5)];
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (this.transform.gameObject);
	}
}
