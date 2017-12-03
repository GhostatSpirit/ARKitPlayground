using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionHandler : MonoBehaviour {
    private int topPartMask;

    HealthSystem turretHealth;
    // Use this for initialization
    void Start () {
        topPartMask = LayerMask.GetMask("TopPart");
        turretHealth = GetComponent<HealthSystem>();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        GameObject turretGO = collision.collider.gameObject;
        Debug.Log(turretGO);
        if (collision.rigidbody == this.GetComponent<Rigidbody>())
        {
            //Debug.Log("return");
            return;
        } else
        {
            //Debug.Log("Turret parent: " + turretGO.transform.parent.gameObject.ToString());
            //Debug.Log("this: " + this.gameObject.ToString());
            //Debug.Log(collision.gameObject);
        }

        if (turretGO.tag=="TopPart") {
            
            if (VelocityAlmostZero( collision.rigidbody.velocity ) && VelocityAlmostZero(this.GetComponent<Rigidbody>().velocity )) { 
                //Debug.Log("collision2222 " + collision.gameObject.name);
                return;
            }
            Debug.Log("collision2222 " + collision.gameObject.name);
            HealthSystem hs = turretGO.GetComponent<HealthSystem>();
            if (!hs) {
                hs = turretGO.GetComponent<ParentInfo>().hs;
            }
            if (hs)
            {
                hs.DoDamage(100, collision.gameObject, collision);
            }
        }
    }

    bool VelocityAlmostZero( Vector3 v) {
        if (Mathf.Approximately(v.x, 0) && Mathf.Approximately(v.y, 0) && Mathf.Approximately(v.z, 0))
            return true;
        return false;
    }
}
