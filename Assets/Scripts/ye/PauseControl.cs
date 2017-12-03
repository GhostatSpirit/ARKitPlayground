using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour {

    public GameObject pauseMenu;

    bool active = true;

    Button button;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(setPauseMenu);
    }
	
	// Update is called once per frame
	void Update () {
        active = !pauseMenu.activeInHierarchy;
	}

    void setPauseMenu()
    {
        pauseMenu.SetActive(active);
    }
}
