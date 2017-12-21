using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceController : MonoBehaviour {

    // TEST

    public float ForceAdded;
    private Vector2 ForceDirection;
    private Vector2 DragStartPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            DragStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0)) {
            ForceAdded = 0.0002f * Vector2.Distance(Input.mousePosition, DragStartPos);
            ForceDirection = ((Vector2) Input.mousePosition - DragStartPos).normalized;
        }
        if (Input.GetMouseButtonUp(0)) {
            ForceAdded = 0f;
        }

		//if (Input.GetKey(KeyCode.DownArrow)) {
  //          ForceAdded += 1f * Time.deltaTime;
  //      }
  //      if (Input.GetKeyUp(KeyCode.DownArrow)) {
  //          ForceAdded = 0f;
  //      }
        foreach (var j in GetComponentsInChildren<SpringJoint2D>()) {
            j.connectedAnchor
        }
    }
}
