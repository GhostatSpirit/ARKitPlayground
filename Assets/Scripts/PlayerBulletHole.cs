﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBulletHole : MonoBehaviour {

	public float duration = 1f;
	public float size = 2f;

	HealthSystem playerHealth;

	// Use this for initialization
	void Start () {
		playerHealth = GetComponent<HealthSystem> ();
		playerHealth.OnObjectHurt += showBulletHole;
		CameraPlay.CurrentCamera = Camera.main;
	}

	void showBulletHole(object hurtObj, EventArgs args){
		ObjectHurtEventArgs hargs = (ObjectHurtEventArgs)args;
		Vector3 screenPos = Camera.main.WorldToScreenPoint (hargs.collision.contacts [0].point);
		Vector3 viewportPos = Camera.main.ScreenToViewportPoint (screenPos);
		CameraPlay.BulletHole (viewportPos.x, viewportPos.y, duration, size);
	}

}
