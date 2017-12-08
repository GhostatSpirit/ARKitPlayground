using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSaver : MonoBehaviour {

	public int width { get; private set;}
	public int height { get; private set;}

	// Use this for initialization
	void Start () {
		width = Screen.currentResolution.width;
		height = Screen.currentResolution.height;
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
