using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleBlockScript : MonoBehaviour {
    public Sprite[] PuzzBlockColors;
    public List<Transform> AdjPuzzBlocks;
    public int BlockColorInt;
    public int myRow; public int myCol;
    GameObject myParent;
    SpriteRenderer mySprite;

    // Use this for initialization
    void Start() {
        myParent = transform.gameObject;
        mySprite = myParent.GetComponent<SpriteRenderer>();
        mySprite.sprite = PuzzBlockColors[BlockColorInt];

        CheckAdjBlocks(Vector2.left);
        CheckAdjBlocks(Vector2.up);
        CheckAdjBlocks(Vector2.right);
        CheckAdjBlocks(Vector2.down);
    }

    // Update is called once per frame
    void Update() {
        mySprite.sprite = PuzzBlockColors[BlockColorInt];
        this.name = myRow + "," + myCol; 
    }

    public void CheckAdjBlocks(Vector2 direction) {
        Vector3 modifier = Vector2.zero;
        if(direction == Vector2.left || direction == Vector2.right) {
            if (direction == Vector2.left) {
                AdjPuzzBlocks.Clear();
            }
            modifier = direction * GetComponent<BoxCollider2D>().bounds.size.x;
        } else if (direction == Vector2.up|| direction == Vector2.down) {
            modifier = direction * GetComponent<BoxCollider2D>().bounds.size.y;
        }

        RaycastHit2D blockHit = Physics2D.Raycast(transform.localPosition + modifier, direction, 2f);
        if (blockHit.collider != null && !AdjPuzzBlocks.Contains(blockHit.collider.transform)) {
            AdjPuzzBlocks.Add(blockHit.collider.transform);
        }
    }

    /*
    void OnCollisionEnter2D(Collision2D col2d) {
        if (col2d.gameObject.tag == "Blocks") {
            AdjPuzzBlocks.Add(col2d.gameObject);
        }
    }

     void OnCollisionStay2D(Collision2D col2d) {
         if (col2d.tag == "Blocks") {
             AdjPuzzBlocks.Add(col2d.gameObject);
         }
     } 

     void OnCollisionExit2D(Collision2D col2D) {
         if (col2D.gameObject.tag == "Blocks") {
             foreach (GameObject pBlock in AdjPuzzBlocks){
                 if (pBlock.name == col2D.gameObject.name) {
                     AdjPuzzBlocks.Remove(pBlock);
                 }
             }
         }
     }
     */
}
