using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDefendSystem : MonoBehaviour {
    public float vanishTime;
    public int waitTime;

    private int count;
    private float deltaScale;
    private bool end = false;
    private bool start = true;
    private bool wait = false;
    private Vector3 originScale;
    private int playerProLayer;

    public LayerMask layerMask;

	// Use this for initialization
	void Start () {
        count = (int)(vanishTime / Time.deltaTime);
        deltaScale = transform.localScale.x/count;
        originScale = transform.localScale;
        transform.localScale =  Vector3.zero;
        playerProLayer = LayerMask.GetMask("PlayerProjectile");


    }
	
	// Update is called once per frame
	void Update () {
        if (end == true) { end = false; StartCoroutine(Shrink()); }
        if (start == true) { start = false; StartCoroutine(dilation()); }
        if (wait == true) { wait = false; StartCoroutine(delay()); }
	}
    

    IEnumerator Shrink()
    {
        
        // If the object has arrived, stop the coroutine
        while (transform.localScale.x > 0) { 
            transform.localScale -= deltaScale * new Vector3(1, 1, 1);
            yield return null;
        }
        wait = true;
        // Otherwise, continue next frame
        // transform.localScale = originScale;
        yield break;
        
        
    }

    IEnumerator dilation()
    {

        // If the object has arrived, stop the coroutine
        while (transform.localScale.x < originScale.x)
        {
            transform.localScale += 5*deltaScale * new Vector3(1, 1, 1);
            yield return null;
            
        }
        end = true;
        // Otherwise, continue next frame
        //transform.localScale = originScale;
        yield break;

    }

    IEnumerator delay()
    {
        int count = 0;
        // If the object has arrived, stop the coroutine
        while (count<waitTime)
        {
            //Debug.
            count++;
            yield return null;
           
        }
        start = true;
        // Otherwise, continue next frame
        //transform.localScale = originScale;
        yield break;

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("bullet vanished!？" +     other.gameObject.layer+ layerMask.value);
        bool layerHit = (layerMask.value >> other.gameObject.layer)==1;
        if (layerHit) {
            Debug.Log("bullet vanished!");
            Destroy(other.gameObject);
        }
    }
}
