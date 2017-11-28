// Script based on Sebastian Lague's script
// https://github.com/SebLague/Field-of-View/blob/master/Episode%2001/Scripts/FieldOfView.cs

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {

	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	void Start() {
		StartCoroutine ("FindTargetsWithDelay", .2f);
	}


	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}

	void FindVisibleTargets() {
		visibleTargets.Clear ();
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2f) {
				float dstToTarget = Vector3.Distance (transform.position, target.position);

				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					visibleTargets.Add (target);
				}
			}
		}
	}


	public Vector3 DirFromTransform(Transform target) {
		return (target.position - transform.position).normalized;
	}

	public Vector3 DirFromAngle(float angleInDegrees){
		Vector3 localDir = new Vector3 (Mathf.Sin (angleInDegrees * Mathf.Deg2Rad),
			0f,
			 Mathf.Cos (angleInDegrees * Mathf.Deg2Rad));

		Vector3 globalDir = transform.TransformDirection (localDir);

		float normalRad = Mathf.Tan (viewAngle / 2f * Mathf.Deg2Rad);

		Vector3 result = transform.up + globalDir * normalRad;
		result.Normalize ();

		return result;
	}

}