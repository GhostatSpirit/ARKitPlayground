using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class ResolutionScaler : MonoBehaviour {

	void Start(){

		#if !UNITY_EDITOR

		int maxWidth, maxHeight = 0;

		Resolution[] resolutions = Screen.resolutions;
		foreach(Resolution reso in resolutions){
			maxWidth = reso.width;
			maxHeight = reso.height;
		}

		Debug.Log (maxWidth);
		Debug.Log (maxHeight);

		int newWidth = maxWidth / 4 * 3;
		int newHeight = maxHeight / 4 * 3;

		if(Device.generation == DeviceGeneration.iPad5Gen){
			newWidth = maxWidth / 2;
			newHeight = maxHeight / 2;
		}


		Screen.SetResolution (newWidth, newHeight, true);

		#endif

		Camera.main.depthTextureMode = DepthTextureMode.Depth;
	}

}
