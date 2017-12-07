using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CoreBehavior : MonoBehaviour {

    HealthSystem HS;

    public GameObject congMenu;

    public GameObject playerCube;

    public GameObject cleaner;

    BoxCollider playerCollider;

    BoxCollider cleanerCollider;

    //public int FinalSceneNum;


	// Use this for initialization
	void Start () {
        HS = GetComponent<HealthSystem>();
        playerCollider = playerCube.GetComponent<BoxCollider>();
        cleanerCollider = cleaner.GetComponent<BoxCollider>();
        HS.OnObjectDead += CoreDead;
	}

    void CoreDead(object hurtObj, EventArgs args)
    {
        CoreDie();
    }

    void CoreDie()
    {
//        StartCoroutine(congShow());
        //SceneManager.LoadScene(FinalSceneNum);
		Time.timeScale = 0;

		playerCollider.enabled = false;
		cleanerCollider.enabled = false;
		congMenu.SetActive(true);

    }

    IEnumerator congShow()
    {

        Time.timeScale = 0;
        
        playerCollider.enabled = false;
        cleanerCollider.enabled = false;
        congMenu.SetActive(true);

        yield return null;
        //yield return new WaitForSeconds(3f);

        
    }

}
