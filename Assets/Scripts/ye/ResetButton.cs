using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour {

    public GameObject hitParent;

    ARHitTestNew ARHTN;

    Button button;

    // Use this for initialization

	void Start () {
        ARHTN = hitParent.GetComponent<ARHitTestNew>();
        button = GetComponent<Button>();
        button.onClick.AddListener(startButtonOnClick);
    }
	
    void startButtonOnClick()
    {
        ARHTN.set = false;

        ARHTN.setted = false;
    }

	void Update () {
		
	}
}
