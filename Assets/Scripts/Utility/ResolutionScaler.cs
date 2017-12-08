using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionScaler : MonoBehaviour {

	int originalWidth, originalHeight;

	void Start(){
		var screenPixels = Screen.width * Screen.height;
		Debug.Log ("screenPixels: " + screenPixels.ToString ());
		Debug.Log (Screen.width);
		Debug.Log (Screen.height);

		originalWidth = Screen.width;
		originalHeight = Screen.height;


		if(screenPixels > 2074000){
			Debug.Log ("setting resolution");
			// Screen.SetResolution (Screen.width / 2, Screen.height / 2, true);
			int newWidth = Screen.width / 4 * 3;
			int newHeight = Screen.height / 4 * 3;
			Screen.SetResolution (newWidth, newHeight, true);

		} else {
			int newWidth = Screen.width / 4 * 3;
			int newHeight = Screen.height / 4 * 3;
			Screen.SetResolution (newWidth, newHeight, true);
		}
		Camera.main.depthTextureMode = DepthTextureMode.Depth;
	}
	
	void OnDestory(){
		Screen.SetResolution (originalWidth, originalHeight, true);
	}
}
