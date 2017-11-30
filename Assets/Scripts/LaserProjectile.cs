using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserProjectile : BaseProjectile {

	public float m_maxDistance = 1f;

	public LayerMask m_targetMask;

	float m_deltaDamage = 0f;
	Vector3 m_direction;
	// GameObject m_launcher;

	LineRenderer m_lr;

	// float m_timer = 0f;
// 	bool m_fired = false;

	// Use this for initialization
	void Start () {
		m_lr = GetComponent<LineRenderer> ();
		m_lr.useWorldSpace = true;
		m_lr.enabled = false;

		this.enabled = false;
	}

	void OnDisable(){
		m_lr.enabled = false;
	}

	void Update(){
		m_lr.enabled = true;
		Ray ray = new Ray (transform.position, m_direction);
		RaycastHit hit;
		Vector3 endPosition;

		if (Physics.Raycast (ray, out hit, m_maxDistance, m_targetMask)) {
			endPosition = hit.point;
			//TODO: instantiate particle system for the hit
			HealthSystem hs = hit.transform.GetComponent<HealthSystem> ();
			if(hs){
				m_deltaDamage += m_damage * Time.deltaTime;
				if(m_deltaDamage > 1f){
					int idamage = (int)m_deltaDamage;
					hs.DoDamage (idamage, gameObject, endPosition);
					m_deltaDamage -= (float)idamage;
				}
			}
		} else {
			endPosition = transform.position + m_direction * m_maxDistance;
		}

		Vector3[] linePoints = new Vector3[2];
		linePoints [0] = transform.position;
		linePoints [1] = endPosition;
		m_lr.SetPositions (linePoints);
	}

	public override void FireProjectile(GameObject launcher, GameObject target, Vector3 direction, int damage){
		this.enabled = true;
		m_damage = damage;
		if(launcher){
			// m_launcher = launcher;
			if (direction != Vector3.zero) {
				m_direction = direction.normalized;
			} else {
				m_direction = (target.transform.position - launcher.transform.position).normalized;
			}
		}
	}

}
