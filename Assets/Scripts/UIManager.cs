using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
    GameMangerScript myGM;

	// Use this for initialization
	void Start () {
        myGM = GameObject.Find("GameManager").GetComponent<GameMangerScript>();

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, Vector3.left);
        transform.FindChild("P1Space").GetComponent<RectTransform>().position = screenPoint;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
