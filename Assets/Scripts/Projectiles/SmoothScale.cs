using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothScale : MonoBehaviour {

	public Vector3 targetScale = new Vector3(1f, 1f, 1f);

	public Vector3 initialFactor = new Vector3(1f, 0f, 1f);

	public float scaleSpeed = 0.5f;

	// Use this for initialization
	void Start () {
	}

	public void SetTargetLocalScale(Vector3 targetScale){
//		Debug.Log (targetScale);
		this.targetScale = targetScale;
		Debug.Log (this.targetScale);
		transform.localScale = new Vector3 (targetScale.x * initialFactor.x, 
			targetScale.y * initialFactor.y,
			targetScale.z * initialFactor.z);
//		Debug.Log (transform.localScale);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tempScale = Vector3.Lerp (transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
		transform.localScale = tempScale;
	}
}
