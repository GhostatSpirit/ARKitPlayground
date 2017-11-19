using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectile : NormalProjectile {

	public float rotateSpeed = 5.0f;

	GameObject m_target;

	// Update is called once per frame
	protected override void Update () {
		if(m_fired){
			if(m_target){
				Vector3 toTarget = m_target.transform.position - transform.position;
				Vector3 dir = Vector3.RotateTowards (transform.forward, toTarget, rotateSpeed, 0.0f);
				transform.forward = dir;
				m_direction = dir;
			}
			transform.position += m_direction * m_speed * Time.deltaTime;
		}

		if(Vector3.Distance(m_launcher.transform.position, transform.position) > m_destroyDistance){
			Destroy (this.gameObject);
		}

	}

	public override void FireProjectile(GameObject launcher, GameObject target, Vector3 direction, int damage){
		m_damage = damage;

		m_launcher = launcher;
		m_target = target;

		if(launcher){
			m_launcher = launcher;
			if (direction != Vector3.zero) {
				m_direction = direction;
			} else if(launcher != null && target != null) {
				m_direction = (target.transform.position - launcher.transform.position).normalized;
			} else {
				m_direction = Vector3.forward;
			}

			transform.forward = m_direction;

			m_fired = true;
		}
	}
}
