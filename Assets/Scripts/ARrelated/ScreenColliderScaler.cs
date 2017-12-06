using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
public class ScreenColliderScaler : MonoBehaviour {

	[Range(0f, 1f)]
	public float widthFactor = 0.9f;

	[Range(0f, 1f)]
	public float heightFactor = 0.9f;

	public float localDepth = 0.005f;

	void Awake(){
		float nearDistance = transform.localPosition.z;

		float widthSideEdge = (1.0f - widthFactor) / 2.0f;
		float heightSideEdge = (1.0f - heightFactor) / 2.0f;

		Vector3 lowerleftWorld = 
			Camera.main.ViewportToWorldPoint (new Vector3(widthSideEdge, heightSideEdge, nearDistance));
		Vector3 upperRightWorld = 
			Camera.main.ViewportToWorldPoint (new Vector3(1f - widthSideEdge, 1f - heightSideEdge, nearDistance));

		Vector3 lowerleft = transform.InverseTransformPoint (lowerleftWorld);
		Vector3 upperRight = transform.InverseTransformPoint (upperRightWorld);

		BoxCollider box = GetComponent<BoxCollider> ();

		float width = Mathf.Abs (upperRight.x - lowerleft.x);
		float height = Mathf.Abs (upperRight.y - lowerleft.y);

		box.size = new Vector3 (width, height, localDepth);
		box.center = new Vector3 (0f, 0f, localDepth / 2.0f);
	}

}
