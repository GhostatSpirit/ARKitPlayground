using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetButton : MonoBehaviour {

    public GameObject hitParent;

    ARHitTestNew ARHT;

    Button button;

    // Use this for initialization

	void Start () {
        ARHT = hitParent.GetComponent<ARHitTestNew>();
        button = GetComponent<Button>();
        button.onClick.AddListener(startButtonOnClick);
    }
	
    void startButtonOnClick()
    {
        ARHT.set = true;

        // ARHT.setted = false;
    }

	void Update () {
		
	}
}
