using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour {

	public Transform target;

	public float rotateSpeed = 20f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 toTarget = target.position - transform.position;
		Quaternion qTo = Quaternion.LookRotation (toTarget);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, 
			rotateSpeed * Time.deltaTime);
		
	}

}
