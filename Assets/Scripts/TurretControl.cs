using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour {

	public float scaleInterval = 1f;

	RotateTowards rt;
	ShootingSystem ss;
	LaserShootingSystem lss;
	FieldOfView fov;

	bool isRotating = false;
	bool isShooting = false;


	// Use this for initialization
	void Start () {
		rt = GetComponent<RotateTowards> ();
		ss = GetComponent<ShootingSystem> ();
		lss = GetComponent<LaserShootingSystem> ();
		fov = GetComponent<FieldOfView> ();

		//if(rt)  rt.enabled = isRotating;
		//if(ss)  ss.enabled = isShooting;
		//if(lss) lss.enabled = isShooting;
	}
	
	// Update is called once per frame
	void Update () {
		if (fov) {
			Transform target = fov.GetVisibleTarget (0);
			if (rt)
				rt.target = target;
			if (ss)
				ss.target = target;
			if (lss)
				lss.target = target;
		}

	}

	public void ToggleRotate(){
		isRotating = !isRotating;
        if (rt) rt.enabled = isRotating;
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
