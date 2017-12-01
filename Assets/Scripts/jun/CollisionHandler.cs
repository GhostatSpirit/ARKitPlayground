using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {
    private int topPartMask;
	// Use this for initialization
	void Start () {
        topPartMask = LayerMask.GetMask("TopPart");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.collider.gameObject.tag=="TopPart") {
            Debug.Log("collision!!! " + collision.gameObject.name);
        }
    }
}
