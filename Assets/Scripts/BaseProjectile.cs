using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour {
	public float m_speed = 5.0f;
	public float m_destroyDistance = 3.0f;
	public int m_damage = 10;

	public abstract void FireProjectile(GameObject launcher, GameObject target, Vector3 direction, int damage);

	protected virtual void OnCollisionEnter(Collision coll){
		Debug.Log (coll.transform);
		HealthSystem hs = coll.transform.GetComponentInParent<HealthSystem> ();
		if(hs){
			hs.DoDamage (m_damage, gameObject, coll);
		}
		Destroy (this.gameObject);
	}
}
