﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectile : BaseProjectile {

	public float m_speed = 5.0f;
	public float m_destroyDistance = 3.0f;

	protected Vector3 m_direction;
	protected bool m_fired;

	protected GameObject m_launcher;

	protected Vector3 m_initPos;

	// Update is called once per frame
	protected virtual void Update () {
		if(m_fired){
			transform.position += m_direction * m_speed * Time.deltaTime;
		}
			
		if(isOutOfDistance()){
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
		m_initPos = transform.position;
	}

    public override void FireProjectileNoDirection(GameObject launcher, Vector3 direction, int damage)
    {
        m_damage = damage;
        if (launcher)
        {
            m_launcher = launcher;

            m_direction = direction;
            
            m_fired = true;
        }
        m_initPos = transform.position;
    }


    protected virtual void OnCollisionEnter(Collision coll){
		//Debug.Log (coll.transform);
		HealthSystem hs = coll.collider.GetComponentInParent<HealthSystem> ();
		if(hs){
			hs.DoDamage (m_damage, gameObject, coll);
		}
		Destroy (this.gameObject);
	}

	protected bool isOutOfDistance(){
		return Vector3.Distance (m_initPos, transform.position) > m_destroyDistance;
	}

}
