using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastExperiment : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		if (Input.GetKeyDown (KeyCode.RightShift)) {
			Collider coll = GetComponent<Collider> ();
			coll.enabled = false;

			if (Physics.Raycast (transform.position, Vector3.up, 0.1f)) {
				Debug.Log ("collider still enabled!!");
			} else {
				Debug.Log ("collider disabled!!");
			}
		}
	}
}
