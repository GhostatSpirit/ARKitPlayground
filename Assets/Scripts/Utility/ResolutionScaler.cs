using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class ResolutionScaler : MonoBehaviour {

	int originalWidth = 0, originalHeight = 0;

	public bool scaleInScene = false;

	void Start(){

		#if !UNITY_EDITOR

		GameObject saverGO = GameObject.FindGameObjectWithTag ("Resolution");
		if (saverGO == null)
			return;
		ResolutionSaver saver = saverGO.GetComponent<ResolutionSaver> ();
		if (saver == null)
			return;

		int originalWidth = saver.width;
		int originalHeight = saver.height;

		Debug.Log("originalWidth: " + originalWidth.ToString());
		Debug.Log("originalHeight: " + originalHeight.ToString());

		switch(Device.generation){
		case DeviceGeneration.iPad5Gen:
			if(scaleInScene){
				Screen.SetResolution (originalWidth / 2, originalHeight / 2, true);
			} else {
				Screen.SetResolution (originalWidth , originalHeight, true);
			}
			break;
		case DeviceGeneration.iPadPro1Gen:
			if(scaleInScene){
				Screen.SetResolution (originalWidth / 2, originalHeight / 2, true);
			} else {
				Screen.SetResolution (originalWidth , originalHeight, true);
			}
			break;
		case DeviceGeneration.iPhone6S:
			if(scaleInScene){
				Screen.SetResolution (originalWidth / 4 * 3, originalHeight / 4 * 3, true);
			} else {
				Screen.SetResolution (originalWidth , originalHeight, true);
			}
			break;
		case DeviceGeneration.iPhone6SPlus:
			if(scaleInScene){
				Screen.SetResolution (originalWidth / 4 * 3, originalHeight / 4 * 3, true);
			} else {
				Screen.SetResolution (originalWidth , originalHeight, true);
			}
			break;
		}


		Debug.Log("newWidth: " + Screen.currentResolution.width.ToString());
		Debug.Log("newHeight: " +Screen.currentResolution.height.ToString());

		#endif

		Camera.main.depthTextureMode = DepthTextureMode.Depth;
	}

	void OnDisable(){
		
	}

}
