using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDefendSystem : MonoBehaviour {
    public float Scale;
    public float ShrinkTime;
    public float DilateTime;
    public float DelayTimeMin;
    public float DelayTimeMax;

    [HideInInspector]
    public  ProtectRender prender;

    private int count;
    private float deltaScaleShrink;
    private float deltaScaleDilate;
    private bool end = false;
    private bool start = true;
    private bool wait = false;
    private Vector3 originScale;
    private int playerProLayer;
    AudioSource hitSound;

    public LayerMask layerMask;

	// Use this for initialization
	void Start () {
        count = (int)(ShrinkTime / Time.deltaTime);
        deltaScaleShrink = transform.localScale.x/count;
        count = (int)(DilateTime / Time.deltaTime);
        deltaScaleDilate = transform.localScale.x / count;
        transform.localScale = new Vector3(Scale, Scale, Scale);
        originScale = transform.localScale;
        transform.localScale =  Vector3.zero;
        prender.active = true;
        hitSound = GetComponent<AudioSource>();
        StartCoroutine(Activate());


    }
	
	// Update is called once per frame
	void Update () {
        //if (end == true) { end = false; StartCoroutine(Shrink()); }
        //if (start == true) { start = false; StartCoroutine(dilation()); }
        //if (wait == true) { wait = false; StartCoroutine(delay()); }
	}

    IEnumerator Activate()
    {
        while (true)
        {
            prender.active = true;
            // If the object has arrived, stop the coroutine
            while (transform.localScale.x < originScale.x)
            {
                transform.localScale += deltaScaleDilate * new Vector3(1, 1, 1);
                yield return null;

            }

            yield return new WaitForSeconds(DelayTimeMax);
            prender.active = false;

            
            while (transform.localScale.x > 0)
            {
                transform.localScale -= deltaScaleShrink * new Vector3(1, 1, 1);
                yield return null;
            }

            yield return new WaitForSeconds(DelayTimeMin);

            
           
        }
       // yield break;


    }

    //IEnumerator Shrink()
    //{
        
    //    // If the object has arrived, stop the coroutine
    //    while (transform.localScale.x > 0) { 
    //        transform.localScale -= deltaScale * new Vector3(1, 1, 1);
    //        yield return null;
    //    }
    //    wait = true;
    //    // Otherwise, continue next frame
    //    // transform.localScale = originScale;


    //    yield break;
        
        
    //}

    //IEnumerator dilation()
    //{
    //    prender.active = true;
    //    // If the object has arrived, stop the coroutine
    //    while (transform.localScale.x < originScale.x)
    //    {
    //        transform.localScale += 5*deltaScale * new Vector3(1, 1, 1);
    //        yield return null;
            
    //    }
    //    end = true;
    //    // Otherwise, continue next frame
    //    //transform.localScale = originScale;
    //    prender.active = false;
    //    yield break;

    //}

    //IEnumerator delay()
    //{
        
    //    int count = 0;
    //    // If the object has arrived, stop the coroutine
    //    while (count<waitTime)
    //    {
    //        //Debug.
    //        count++;
    //        yield return null;
           
    //    }
    //    start = true;
    //    // Otherwise, continue next frame
    //    //transform.localScale = originScale;
        
    //    yield break;

    //}

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("bullet vanished!？" +     other.gameObject.layer+ layerMask.value);
        bool layerHit = (layerMask.value >> other.gameObject.layer)==1;
        if (layerHit) {
            //Debug.Log("bullet vanished!");
            hitSound.Play();
            Destroy(other.gameObject);
        }
    }
}
