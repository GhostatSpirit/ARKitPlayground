using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlood : MonoBehaviour {

    public GameObject player;
    Image image;
    HealthSystem HS;

	// Use this for initialization
	void Start () {
        HS = player.GetComponent<HealthSystem>();
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        
        image.fillAmount = (float) HS.currentHealth / (float) HS.maxHealth;
        //Debug.Log(image.fillAmount);

    }
}
