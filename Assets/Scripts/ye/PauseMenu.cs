using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;

    public GameObject failMenu;

    public HealthSystem playerHS;

    bool show = false;

    Image image;
    Color targetColor = new Color(1, 1, 1, 1);

	// Use this for initialization
	void Start () {     
        image = failMenu.GetComponent<Image>();
        //targetColor = image.color;
        playerHS.OnObjectDead += ShowFail;  
	}

    void ShowFail(object hurtObj, EventArgs args)
    {
        ShowFailMenu();
    }

    void ShowFailMenu()
    {
        failMenu.SetActive(true);
        show = true;
    }


    // Update is called once per frame
    void Update () {
        
//        if (pauseMenu.activeInHierarchy == true || failMenu.activeInHierarchy == true)
//        {
//            Time.timeScale = 0;
//        }
//        else
//        {
//			Debug.Log ("reset timeScale in PauseMenu");
//            Time.timeScale = 1;
//        }

        if (show == true)
        {
            image.color = Color.Lerp(image.color, targetColor, 0.03f);
            //show = false;
        }
        //if((targetColor.a - image.color.a  < 0.05))
        //{
        //    Time.timeScale = 0;
        //}
    }

    public void setTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
