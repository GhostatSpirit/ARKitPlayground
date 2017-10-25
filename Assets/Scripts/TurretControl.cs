using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour {

	public float scaleInterval = 1f;

	RotateTowards rt;
	ShootingSystem ss;

	bool isRotating = false;
	bool isShooting = false;


	// Use this for initialization
	void Start () {
		rt = GetComponent<RotateTowards> ();
		ss = GetComponent<ShootingSystem> ();
		rt.enabled = isRotating;
		ss.enabled = isShooting;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleRotate(){
		isRotating = !isRotating;
		rt.enabled = isRotating;
	}

	public void ToggleShoot(){
		isShooting = !isShooting;
		ss.enabled = isShooting;
	}

	public void Bigger(){
		transform.parent.localScale += new Vector3 (scaleInterval, scaleInterval, scaleInterval);
	}

	public void Smaller(){
		if (transform.localScale.x > 1f) {
			transform.parent.localScale -= new Vector3 (scaleInterval, scaleInterval, scaleInterval);
		}
	}
}
