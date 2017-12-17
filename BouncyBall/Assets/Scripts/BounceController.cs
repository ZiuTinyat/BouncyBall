using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceController : MonoBehaviour {

    // TEST

    public float ForceAdded;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.DownArrow)) {
            ForceAdded += 1f * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            ForceAdded = 0f;
        }
        foreach (var rb in GetComponentsInChildren<Rigidbody2D>()) {
            rb.AddForce(ForceAdded * Vector2.down, ForceMode2D.Force);
        }
    }
}
