using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerFuncControl : MonoBehaviour {
    private bool activated = false;
    public GameObject Barrier;

    private GameObject instantiateBarrier;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void BarrierActivate() {
        Debug.Log("barrier!!!!!!!!");
        instantiateBarrier = Instantiate(Barrier, this.transform.position, this.transform.rotation, transform);
    }




}
