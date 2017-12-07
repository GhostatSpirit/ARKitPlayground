using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBulletHole : MonoBehaviour {

	public float duration = 1f;
	public float size = 2f;

    AudioSource audioSource;

	public DecalEmitter emitter;

	HealthSystem playerHealth;

    

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
		playerHealth = GetComponent<HealthSystem> ();
		playerHealth.OnObjectHurt += showBulletHole;
		CameraPlay.CurrentCamera = Camera.main;
	}

	void showBulletHole(object hurtObj, EventArgs args){
		ObjectHurtEventArgs hargs = (ObjectHurtEventArgs)args;
		Collision coll = hargs.collision;
		if (coll != null && emitter != null) {
            //			Vector3 screenPos = Camera.main.WorldToScreenPoint (coll.contacts [0].point);
            //			Vector3 viewportPos = Camera.main.ScreenToViewportPoint (screenPos);
            //
            //			CameraPlay.BulletHole (viewportPos.x, viewportPos.y, duration, size);

            if (audioSource != null)
            {
                audioSource.Play();
            }

            emitter.EmitDecal (coll.contacts [0].point);
		}
	}

}
