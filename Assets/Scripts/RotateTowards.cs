using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour {

	public Transform target;
	public Transform yawSegment;
	public Transform pitchSegment;

	Quaternion yawSegmentStartRotation;
	Quaternion pitchSegmentStartRotation;

	public float yawSpeed = 30.0f;
	public float yawLimit = 90.0f;
	public float pitchSpeed = 30.0f;
	public float pitchLimit = 90.0f;

	public bool isPitchSymmetric = true;

	// Use this for initialization
	void Start () {
		yawSegmentStartRotation = yawSegment.localRotation;
		pitchSegmentStartRotation = pitchSegment.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
//		Vector3 toTarget = target.position - transform.position;
//		Quaternion qTo = Quaternion.LookRotation (toTarget);
//		transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, 
//			rotateSpeed * Time.deltaTime);
		Vector3 targetRelative;
		float angle;
		Quaternion targetRotation;

		if (yawSegment && yawLimit != 0f) {
			targetRelative = yawSegment.InverseTransformPoint (target.position);
			angle = Mathf.Atan2 (targetRelative.x, targetRelative.z) * Mathf.Rad2Deg;

			if (isPitchSymmetric) {
				if (angle >= 90f)
					angle = -(180f - angle);	
				if (angle <= -135f)
					angle = -(-180f + angle);
			}

			Quaternion yRot = Quaternion.Euler (0f, Mathf.Clamp (angle, -yawSpeed * Time.deltaTime, yawSpeed * Time.deltaTime), 0f);
			targetRotation = yawSegment.rotation * yRot;

			if (yawLimit < 360f && yawLimit > 0f)
				yawSegment.rotation = 
				Quaternion.RotateTowards (yawSegment.parent.rotation * yawSegmentStartRotation, targetRotation, yawLimit);
			else
				yawSegment.rotation = targetRotation;
		}
		
		if (pitchSegment && pitchLimit != 0f) {
			targetRelative = pitchSegment.InverseTransformPoint (target.position);
			angle = -Mathf.Atan2 (targetRelative.y, targetRelative.z) * Mathf.Rad2Deg;
			if (angle >= 180f)
				angle = 180f - angle;	
			if (angle <= -180f)
				angle = -180f + angle;
			targetRotation = pitchSegment.rotation *
			Quaternion.Euler (Mathf.Clamp (angle, -pitchSpeed * Time.deltaTime, pitchSpeed * Time.deltaTime), 0f, 0f);
			if (pitchLimit < 360f && pitchLimit > 0f)
				pitchSegment.rotation = 
				Quaternion.RotateTowards (pitchSegment.parent.rotation * pitchSegmentStartRotation, targetRotation, pitchLimit);
			else
				pitchSegment.rotation = targetRotation;
		}

	}

}
