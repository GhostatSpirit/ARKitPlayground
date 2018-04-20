using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeScale : MonoBehaviour {

	public float timeScale = 1f;

	// Use this for initialization
	void Start () {
		Time.timeScale = timeScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
