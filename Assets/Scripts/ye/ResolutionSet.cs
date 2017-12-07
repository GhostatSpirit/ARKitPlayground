using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSet : MonoBehaviour {

    public GameObject machineGunL_43;
    public GameObject machineGunR_43;
    public GameObject machineGunL_169;
    public GameObject machineGunR_169;

    float currentResolution;

 //   // Use this for initialization
 //   void Start () {
		
	//}
	
	// Update is called once per frame
	void Start () {

        currentResolution = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;

        if (currentResolution >= (float)(16 / 9))
        {
            if(machineGunL_169.activeInHierarchy == false)
            {
                machineGunL_169.SetActive(true);
            }
            if (machineGunR_169.activeInHierarchy == false)
            {
                machineGunR_169.SetActive(true);
            }
        }
        else if (currentResolution < (float)(16 / 9) && currentResolution > (float)(4 / 3))
        {
            if (machineGunL_169.activeInHierarchy == false)
            {
                machineGunL_169.SetActive(true);
            }
            if (machineGunR_169.activeInHierarchy == false)
            {
                machineGunR_169.SetActive(true);
            }
        }
        if (currentResolution <= (float)(4 / 3))
        {
            if (machineGunL_43.activeInHierarchy == false)
            {
                machineGunL_43.SetActive(true);
            }
            if (machineGunR_43.activeInHierarchy == false)
            {
                machineGunR_43.SetActive(true);
            }
        }

    }
}
