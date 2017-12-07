using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;

    public GameObject failMenu;

    public HealthSystem playerHS;

	// Use this for initialization
	void Start () {
        playerHS.OnObjectDead += ShowFail;
        
	}

    void ShowFail(object hurtObj, EventArgs args)
    {
        ShowFailMenu();
    }

    void ShowFailMenu()
    {
        failMenu.SetActive(true);
    }


    // Update is called once per frame
    void Update () {
        
        if (pauseMenu.activeInHierarchy == true || failMenu.activeInHierarchy == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void setTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
