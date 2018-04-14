using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectile : NormalProjectile {

	public float rotateSpeed = 5.0f;

	public float suicideDistance = 0.05f;

	GameObject m_target;

	// Update is called once per frame
	protected override void FixedUpdate () {
		if(m_fired){
			if(m_target){
				Vector3 toTarget = m_target.transform.position - transform.position;
				float rotateDelta = rotateSpeed * Time.fixedDeltaTime;
				Vector3 dir = Vector3.RotateTowards (transform.forward, toTarget, rotateDelta, 0.0f);

//				transform.forward = dir;
				m_direction = dir;

				Quaternion newRot = Quaternion.LookRotation (dir);
				rigidbody.MoveRotation (newRot);

			}

			rigidbody.velocity = m_direction * m_speed;

//			Vector3 targetPos = transform.position + m_direction * m_speed * Time.fixedDeltaTime;
//			rigidbody.MovePosition (targetPos);
		}

		if(isOutOfDistance() || isInSuicideDistance()){
			Destroy (this.gameObject);
		}


	}

	public override void FireProjectile(GameObject launcher, GameObject target, Vector3 direction, int damage){
		m_damage = damage;

		m_launcher = launcher;
		m_target = target;

//		Debug.Log (m_target);

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

	protected override void OnCollisionEnter(Collision coll){
		//Vector3 normal = - coll.collider.transform.forward;
		//Vector3 velocity = rigidbody.velocity;

		//Quaternion deltaRot = Quaternion.FromToRotation (normal, velocity);
		//float angle = Quaternion.Angle (deltaRot, Quaternion.identity);

		//Debug.Log ("tracking projectile hit, angle: " + angle.ToString ());

		//base.OnCollisionEnter (coll);
	}

	bool isInSuicideDistance(){
		if(m_target == null){
			return false;
		}
		return Vector3.Distance (m_target.transform.position, transform.position) <= suicideDistance;
	}
}
