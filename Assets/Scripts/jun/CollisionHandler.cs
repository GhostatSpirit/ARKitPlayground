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
            Debug.Log("collision2222 " + collision.gameObject.name);
            HealthSystem hs = turretGO.GetComponent<HealthSystem>();
            if (hs)
            {
                hs.DoDamage(100, collision.gameObject, collision);
            }
        }
    }

    
}
