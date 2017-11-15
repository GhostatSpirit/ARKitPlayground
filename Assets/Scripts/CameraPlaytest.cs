using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlaytest : MonoBehaviour {

	public Transform arCamera;
	public Transform editorCamera;

	public Transform mountToCamera;

	// bool updated = false;

	// Use this for initialization
	void Start () {
#if !UNITY_EDITOR
		// when we are on the iOS device...
		arCamera.gameObject.SetActive(true);
		editorCamera.gameObject.SetActive(false);
		if(mountToCamera != null){
			foreach(Transform child in mountToCamera){
				child.setParent(arCamera);
			}
		}
#else
		// when we are in editor...
		editorCamera.gameObject.SetActive(true);
		arCamera.gameObject.SetActive(false);
		if(mountToCamera != null){
			foreach(Transform child in mountToCamera){
				child.SetParent(editorCamera);
				Debug.Log(child);
			}
		}
#endif

	}
	
//	// Update is called once per frame
//	void Update () {
//		
//	}
}
