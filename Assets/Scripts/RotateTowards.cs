using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour {

	public Transform target;

	public float rotateSpeed = 20.0f;
	public float maxRotation = 90.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		Vector3 toTarget = target.position - transform.position;
//		Quaternion qTo = Quaternion.LookRotation (toTarget);
//		transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, 
//			rotateSpeed * Time.deltaTime);

		Vector3 toTarget = target.position - transform.position;
		// transform the vector into parent's local space
		toTarget = transform.parent.InverseTransformVector (toTarget);
		Quaternion qTo = Quaternion.LookRotation (toTarget, transform.parent.up);
		Quaternion unclampedRot = Quaternion.RotateTowards (transform.localRotation, qTo, 
						rotateSpeed * Time.deltaTime);
		Vector3 unclampedEulerRaw = unclampedRot.eulerAngles;

//		Debug.Log (string.Format ("Rotate: x:{0:0.######} y:{1:0.######} z:{2:0.######}", 
//			unclampedEulerRaw.x, unclampedEulerRaw.y, unclampedEulerRaw.z));


		Vector3 unclampedEuler = new Vector3 (
			unclampedEulerRaw.x < 180.0f ? unclampedEulerRaw.x : unclampedEulerRaw.x - 360f,
			unclampedEulerRaw.y < 180.0f ? unclampedEulerRaw.y : unclampedEulerRaw.y - 360f,
			unclampedEulerRaw.z < 180.0f ? unclampedEulerRaw.z : unclampedEulerRaw.z - 360f
		);

		Quaternion clampedRot = new Quaternion();
		clampedRot.eulerAngles = new Vector3 (
			Mathf.Clamp (unclampedEuler.x, -maxRotation, maxRotation),
			Mathf.Clamp (unclampedEuler.y, -maxRotation, maxRotation),
			unclampedEuler.z
		);
		transform.localRotation = clampedRot;
	}

}
