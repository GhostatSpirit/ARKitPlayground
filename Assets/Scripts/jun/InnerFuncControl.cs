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
        ProtectRender signalSphere= GetComponentInChildren<ProtectRender>();
            
        Transform parentCube=null;
        //Debug.Log("barrier!!!!!!!!");
        foreach (Transform i in transform) {
            if (i.name == "cube") {
                parentCube = i;
            }
        }
        if (parentCube != null) {
            instantiateBarrier = Instantiate(Barrier, this.transform.position, this.transform.rotation, parentCube);
            instantiateBarrier.GetComponent<BarrierDefendSystem>().prender = signalSphere;
        }
    }




}
