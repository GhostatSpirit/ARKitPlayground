using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public struct ARRaycastHit
{
    public enum HitType { NoHit, UnityHit, ARHit };

    public Vector3 point;
    public Vector3 normal;
    public Transform transform;
    public HitType type;
    public RaycastHit hit;

    public ARRaycastHit
        (Vector3 _point, Vector3 _normal, Transform _transform, HitType _type)
    {
        point = _point;
        normal = _normal;
        transform = _transform;
        type = _type;
        hit = new RaycastHit();
    }

    public ARRaycastHit
        (Vector3 _point, Vector3 _normal, Transform _transform, HitType _type, RaycastHit _hit)
    {
        point = _point;
        normal = _normal;
        transform = _transform;
        type = _type;
        hit = _hit;
    }

    public ARRaycastHit(Vector3 _point)
    {
        point = _point;
        normal = Vector3.up;
        transform = null;
        type = HitType.NoHit;
        hit = new RaycastHit();
    }
}

public class ARScreenRaycast : MonoBehaviour {

	public float defaultDistance = 1.0f;
	public float minDistance = 0.2f;
	// public Collider playerCollider;
	public LayerMask layerMask;

	public Vector3 hitPoint { get; private set; }
    public ARRaycastHit arhit { get; private set; }

    bool clampDistance = true;
	
	// Update is called once per frame
	void Update () {
		// first try to hit with Unity raycast
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)){
			hitPoint = ClampHitPointDistance (hit.point);

            arhit = new ARRaycastHit(hitPoint, hit.normal, hit.transform, 
                                     ARRaycastHit.HitType.UnityHit, hit);

//			Debug.Log ("ARScreenRaycast: Got Unity hit");
//			Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", 
//				hitPoint.x, hitPoint.y, hitPoint.z));
//			Debug.Log (hit.transform.name);
			return;
		}

		ARPoint point = new ARPoint {
			x = 0.5f,
			y = 0.5f
		};
		// then try to hit with ARKit ray
		// prioritize reults types
		ARHitTestResultType[] resultTypes = {
			// ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
			// if you want to use infinite planes use this:
			ARHitTestResultType.ARHitTestResultTypeExistingPlane,
			ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
			// ARHitTestResultType.ARHitTestResultTypeFeaturePoint
		}; 

		foreach (ARHitTestResultType resultType in resultTypes)
		{
			Vector3 arHitPoint;
			if (HitTestWithResultType (point, resultType, out arHitPoint))
			{
				hitPoint = ClampHitPointDistance(arHitPoint);
                // assume that we only detect horizontal plane
                arhit = new ARRaycastHit(hitPoint, Vector3.up, null, 
                                         ARRaycastHit.HitType.ARHit);
                return;
			}
		}

		// no hit, just give the point of default distance
		hitPoint = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, defaultDistance));
        arhit = new ARRaycastHit(hitPoint);
	}

	bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes, out Vector3 arHitPoint)
	{
		arHitPoint = Vector3.zero;
		List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
		if (hitResults.Count > 0) {
			foreach (var hitResult in hitResults) {
                arHitPoint = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                //				Debug.Log ("ARScreenRaycast: Got AR hit");
                //				arHitPoint = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
                //				Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", arHitPoint.x, arHitPoint.y, arHitPoint.z));
                return true;
			}
		}
		return false;
	}

	Vector3 ClampHitPointDistance(Vector3 original){
        if (!clampDistance)
        {
            return original;
        }
		else if(Vector3.Distance(Camera.main.transform.position, original) >= minDistance){
			return original;
		} else {
			return Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, minDistance));
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		Gizmos.DrawLine (Camera.main.transform.position, hitPoint);
	}
}
