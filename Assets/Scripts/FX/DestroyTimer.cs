using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour {

	public float destroyTime = 4.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine (DestroyTimerIE (destroyTime));
	}

	IEnumerator DestroyTimerIE(float time){
		yield return new WaitForSeconds (time);
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
