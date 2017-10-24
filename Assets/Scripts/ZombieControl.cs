using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieControl : MonoBehaviour {

	private new Animation animation;
	private bool shouldMove = false;

	// Use this for initialization
	void Start () {
		animation = GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(shouldMove){
			transform.Translate (Vector3.forward * Time.deltaTime * (transform.localScale.x * .05f));
		}
	}

	public void Walk(){
		if (!animation.isPlaying) {
			animation.Play ();
			shouldMove = true;
		} else {
			animation.Stop ();
			shouldMove = false;
		}
	}

	public void LookAt(){
		transform.LookAt (Camera.main.transform);
		transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
	}

	public void Bigger(){
		transform.localScale += new Vector3 (1f, 1f, 1f);
	}

	public void Smaller(){
		if (transform.localScale.x > 1f) {
			transform.localScale -= new Vector3 (1f, 1f, 1f);
		}
	}
}
