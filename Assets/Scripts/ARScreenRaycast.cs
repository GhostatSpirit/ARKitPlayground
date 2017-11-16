using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class ARScreenRaycast : MonoBehaviour {

	public float defaultDistance = 1.0f;
	// public Collider playerCollider;
	public LayerMask layerMask;

	public Vector3 hitPoint { get; private set; }
	
	// Update is called once per frame
	void Update () {
		// first try to hit with Unity raycast
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)){
			hitPoint = hit.point;
			Debug.Log ("ARScreenRaycast: Got Unity hit");
			Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", 
				hitPoint.x, hitPoint.y, hitPoint.z));
			Debug.Log (hit.transform.name);
			return;
		}

		ARPoint point = new ARPoint {
			x = 0.5f,
			y = 0.5f
		};
		// then try to hit with ARKit ray
		// prioritize reults types
		ARHitTestResultType[] resultTypes = {
			ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
			// if you want to use infinite planes use this:
			//ARHitTestResultType.ARHitTestResultTypeExistingPlane,
			ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
			ARHitTestResultType.ARHitTestResultTypeFeaturePoint
		}; 

		foreach (ARHitTestResultType resultType in resultTypes)
		{
			Vector3 arHitPoint;
			if (HitTestWithResultType (point, resultType, out arHitPoint))
			{
				hitPoint = arHitPoint;
				return;
			}
		}

		// no hit, just give the point of default distance
		hitPoint = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, defaultDistance));

	}

	bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes, out Vector3 arHitPoint)
	{
		arHitPoint = Vector3.zero;
		List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
		if (hitResults.Count > 0) {
			foreach (var hitResult in hitResults) {
				Debug.Log ("ARScreenRaycast: Got AR hit");
				arHitPoint = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
				Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", arHitPoint.x, arHitPoint.y, arHitPoint.z));
				return true;
			}
		}
		return false;
	}
}
