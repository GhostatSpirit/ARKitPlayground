using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour {

    //public float shootReverseSpeed;
    
    public GameObject gun;

    GunTime GT;

    Animation anim;

    Animator animator;

    AnimatorStateInfo currentState;

    float shootTime;

    float overHeatTime;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        GT = gun.GetComponent<GunTime>();
        overHeatTime = GT.OverheatTime;
	}
	
	// Update is called once per frame
	void Update () {
        currentState = animator.GetCurrentAnimatorStateInfo(0);
        if(currentState.IsName("Idle"))
        {

        }
        else if (currentState.IsName("Shoot"))
        {
            
            if (currentState.normalizedTime > 1)
            {
                Debug.Log("overheat!!");
                animator.SetBool("Overheat", true);
            }
            else if (currentState.normalizedTime < 0)
            {
                Debug.Log("Go idle!!");
                animator.SetBool("Idle", true);
                
            }
        }
        else if (currentState.IsName("Overheat"))
        {
            
            if (currentState.normalizedTime > overHeatTime)
            {
                Debug.Log("overheat end!!");
                animator.SetBool("Overheat", false);
            }

        }

    }

    public void PlayShoot()
    {
        Debug.Log((float)1 / (float)overHeatTime);
        animator.SetFloat("ShootPlaySpeed", (float)2 / (float) overHeatTime);
        animator.SetBool("Idle", false);
        animator.SetBool("Shoot", true); 
    }
    public void PlayShootReverse()
    {
        animator.SetFloat("ShootPlaySpeed", -(float)2 / (float)overHeatTime);
        animator.SetBool("Shoot", false);
    }
}
