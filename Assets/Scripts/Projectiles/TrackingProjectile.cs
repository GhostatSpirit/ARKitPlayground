using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectile : NormalProjectile {

	public float rotateSpeed = 5.0f;

	GameObject m_target;

	// Update is called once per frame
	protected override void FixedUpdate () {
		if(m_fired){
			if(m_target){
				Vector3 toTarget = m_target.transform.position - transform.position;
				float rotateDelta = rotateSpeed * Time.fixedDeltaTime;
				Vector3 dir = Vector3.RotateTowards (transform.forward, toTarget, rotateDelta, 0.0f);

				Quaternion newRot = new Quaternion ();
				newRot.SetFromToRotation (Vector3.up, dir);
				rigidbody.MoveRotation (newRot);

				// transform.forward = dir;
				m_direction = dir;
			}
			Vector3 targetPos = transform.position + m_direction * m_speed * Time.fixedDeltaTime;
			rigidbody.MovePosition (targetPos);
		}

		if(isOutOfDistance()){
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

		m_initPos = transform.position;
	}
}
