using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalEmitter : MonoBehaviour {

	ParticleSystem system;

	public void Start(){
		system = GetComponent<ParticleSystem> ();
	}

	public void EmitDecal(Vector3 worldHitPoint){
		Vector3 hitScreenPos = Camera.main.WorldToScreenPoint (worldHitPoint);
		Vector3 psScreenPos = Camera.main.WorldToScreenPoint (transform.position);

		hitScreenPos = new Vector3 (hitScreenPos.x, hitScreenPos.y, psScreenPos.z);
		Vector3 clampedWorldHit = Camera.main.ScreenToWorldPoint (hitScreenPos);

		Vector3 psLocalHit = transform.InverseTransformPoint (clampedWorldHit);

		var emitParams = new ParticleSystem.EmitParams ();
		emitParams.position = psLocalHit;


		if(system){
//			Debug.Log (psLocalHit);
			system.Emit (emitParams, 1);
		}
	}
}
