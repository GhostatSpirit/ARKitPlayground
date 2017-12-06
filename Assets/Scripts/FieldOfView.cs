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

	public Transform pivot{
		get{
			if (_pivot == null)
				return transform;
			else
				return _pivot;
		}
		set{
			_pivot = value;
		}
	}

	public Transform _pivot;

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
		Collider[] targetsInViewRadius = Physics.OverlapSphere (pivot.position, viewRadius, targetMask);
		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - pivot.position).normalized;
			if (Vector3.Angle (pivot.up, dirToTarget) < viewAngle / 2f) {
				float dstToTarget = Vector3.Distance (pivot.position, target.position);

				RaycastHit hit;
				if (!Physics.Raycast (pivot.position, dirToTarget, out hit, dstToTarget, obstacleMask)) {
					//Debug.Log (target);
					visibleTargets.Add (target.parent);
				} 
			}
		}
	}


	public Vector3 DirFromTransform(Transform target) {
		return (target.position - pivot.position).normalized;
	}

	public Vector3 DirFromAngle(float angleInDegrees){
		Vector3 localDir = new Vector3 (Mathf.Sin (angleInDegrees * Mathf.Deg2Rad),
			 0f,
			 Mathf.Cos (angleInDegrees * Mathf.Deg2Rad));

		Vector3 globalDir = pivot.TransformDirection (localDir);

		float normalRad = Mathf.Tan (viewAngle / 2f * Mathf.Deg2Rad);

		Vector3 result = pivot.up + globalDir * normalRad;
		result.Normalize ();

		return result;
	}

	public Transform GetVisibleTarget(int index){
		if(index >= visibleTargets.Count || index < 0){
			return null;
		} else {
			return visibleTargets [index];
		}
	}

}