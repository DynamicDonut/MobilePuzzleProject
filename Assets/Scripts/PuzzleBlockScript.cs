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
    }

    // Update is called once per frame
    void Update() {
        mySprite.sprite = PuzzBlockColors[BlockColorInt];
        this.name = myRow + "," + myCol;
        //CheckAdjacentBlocks(); 
        CheckAdjBlocks(Vector2.left);
        CheckAdjBlocks(Vector2.up);
        CheckAdjBlocks(Vector2.right);
        CheckAdjBlocks(Vector2.down);
    }

    void CheckAdjacentBlocks() {
        RaycastHit2D leftBlockHit = Physics2D.Raycast(transform.position, Vector2.left, 2f);
        RaycastHit2D topBlockHit = Physics2D.Raycast(transform.position, Vector2.up, 2f);
        RaycastHit2D rightBlockHit = Physics2D.Raycast(transform.position, Vector2.right, 2f);
        RaycastHit2D bottomBlockHit = Physics2D.Raycast(transform.position, Vector2.down, 2f);

        if (leftBlockHit.collider != null && !AdjPuzzBlocks.Contains(leftBlockHit.collider.transform)) {
           AdjPuzzBlocks.Add(leftBlockHit.collider.transform);
        }
        if (topBlockHit.collider != null && !AdjPuzzBlocks.Contains(topBlockHit.collider.transform)) {
            AdjPuzzBlocks.Add(topBlockHit.collider.transform);
        }
        if (rightBlockHit.collider != null && !AdjPuzzBlocks.Contains(rightBlockHit.collider.transform)) {
            AdjPuzzBlocks.Add(rightBlockHit.collider.transform);
        }
        if (bottomBlockHit.collider != null && !AdjPuzzBlocks.Contains(bottomBlockHit.collider.transform)) {
            AdjPuzzBlocks.Add(bottomBlockHit.collider.transform);
        }
    }

    void CheckAdjBlocks(Vector2 direction) {
        Vector3 modifier = Vector2.zero;
        if(direction == Vector2.left || direction == Vector2.right) {
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
