using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour {

	public GameObject impactPrefab;

	public LayerMask impactMask;
	public LayerMask spawnHoleMask;

	void OnCollisionEnter(Collision coll){
		if(impactMask == (impactMask | (1 <<coll.collider.gameObject.layer))){
			SpawnDecal (coll, impactPrefab);
		}
	}

	void SpawnDecal(Collision coll, GameObject prefab)
	{
		
		if(coll.rigidbody){
			HealthSystem hs = coll.rigidbody.GetComponent<HealthSystem> ();

			if (hs == null) {
				return;
			}

			var contact = coll.contacts [0];

			GameObject spawnedDecal = GameObject.Instantiate(prefab, contact.point, Quaternion.LookRotation(contact.normal));
			spawnedDecal.transform.SetParent(coll.collider.transform);

			ShowDecalSwitch sds = spawnedDecal.GetComponent<ShowDecalSwitch> ();
			if(sds){
				if(spawnHoleMask == (spawnHoleMask | (1 <<coll.collider.gameObject.layer))){
					sds.TurnOn ();
				} else {
					sds.TurnOff ();				}
			}

			DestoryWhenDead dwd = spawnedDecal.GetComponent<DestoryWhenDead> ();
			if(hs && dwd){
				Debug.Log ("added!");
				dwd.SetHealthSystem (hs);
			}
		}
	}

}
