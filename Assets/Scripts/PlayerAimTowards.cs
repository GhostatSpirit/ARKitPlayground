using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimTowards : MonoBehaviour {

	ARScreenRaycast arsr;

	// Use this for initialization
	void Start () {
		arsr = GetComponentInParent<ARScreenRaycast> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(arsr){
			Vector3 towardsTarget = arsr.hitPoint - transform.position;
			if (Vector3.Dot (towardsTarget, Vector3.up) >= 0f) {
				// Debug.Log (">0");
				transform.rotation = Quaternion.LookRotation (towardsTarget.normalized, Camera.main.transform.up);
			} else {
				// Debug.Log ("<0");
				transform.rotation = Quaternion.LookRotation (towardsTarget.normalized, Camera.main.transform.up);
			}
		}
	}
}
