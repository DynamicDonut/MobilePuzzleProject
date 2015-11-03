using UnityEngine;
using System.Collections;

public class CursorMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 v3 = Input.mousePosition;
		v3.z = 0;
		v3 = Camera.main.ScreenToWorldPoint (v3);
		if (Input.GetMouseButtonUp (0)) {
			transform.position = v3;
		}
	}
}
