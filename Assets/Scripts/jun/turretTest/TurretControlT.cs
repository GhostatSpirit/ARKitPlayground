using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControlT : MonoBehaviour {

	public float scaleInterval = 1f;

	RotateTowardsT rt;
	ShootingSystem ss;
	LaserShootingSystem lss;

	public bool isRotating = true;
	bool isShooting = false;


	// Use this for initialization
	void Start () {
		rt = GetComponent<RotateTowardsT> ();
		ss = GetComponent<ShootingSystem> ();
		lss = GetComponent<LaserShootingSystem> ();

		rt.enabled = isRotating;
		if(ss)  ss.enabled = isShooting;
		if(lss) lss.enabled = isShooting;
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
		if(ss)  ss.enabled = isShooting;
		if(lss) lss.enabled = isShooting;
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
