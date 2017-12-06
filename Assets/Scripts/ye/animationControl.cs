using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class animationControl : MonoBehaviour {

    Animator animator;

    Image image;

    bool change = false;

    Color targetColor = new Color(0, 0, 0, 0);

    

	// Use this for initialization
	void Start () {
        //anim = GetComponent<Animation>();

        animator = GetComponent<Animator>();

        image = GetComponent<Image>();

        
	}
	
	// Update is called once per frame
	void Update () {
        if (change == true) {
            image.color = Color.Lerp(image.color, targetColor, Time.deltaTime);
        }
        //animator.GetCurrentAnimatorStateInfo(0).
    }

    public void PlayOnce()
    { 
        animator.SetBool("start", true);
        change = true;
    } 

}
