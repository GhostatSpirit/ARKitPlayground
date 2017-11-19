using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectile : BaseProjectile {

	public float m_speed = 5.0f;
	public float m_destroyDistance = 3.0f;

	protected Vector3 m_direction;
	protected bool m_fired;

	protected GameObject m_launcher;

	// Update is called once per frame
	protected virtual void Update () {
		if(m_fired){
			transform.position += m_direction * m_speed * Time.deltaTime;
		}

		if(Vector3.Distance(m_launcher.transform.position, transform.position) > m_destroyDistance){
			Destroy (this.gameObject);
		}

	}

	public override void FireProjectile(GameObject launcher, GameObject target, Vector3 direction, int damage){
		m_damage = damage;
		if(launcher){
			m_launcher = launcher;
			if (direction != Vector3.zero) {
				m_direction = direction;
			} else {
				m_direction = (target.transform.position - launcher.transform.position).normalized;
			}
			m_fired = true;
		}
	}

	protected virtual void OnCollisionEnter(Collision coll){
		Debug.Log (coll.transform);
		HealthSystem hs = coll.transform.GetComponentInParent<HealthSystem> ();
		if(hs){
			hs.DoDamage (m_damage, gameObject, coll);
		}
		Destroy (this.gameObject);
	}

}
