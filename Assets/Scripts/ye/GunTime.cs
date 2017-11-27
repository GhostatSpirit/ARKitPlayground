using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTime : MonoBehaviour {

    
    [HideInInspector]
    public float shootTime = 0f;

    public float OverheatTime = 2f;

    PlayerShootingNew PSN;

	// Use this for initialization
	void Start () {
        PSN = GetComponent<PlayerShootingNew>();
	}
	
	// Update is called once per frame
	void Update () {

        switch (PSN.enabled)
        {
            case true:
                switch (PSN.Overheat)
                {
                    case true:
                        shootTime -= Time.deltaTime;
                        if (shootTime <= 0)
                        {
                            shootTime = 0;
                            PSN.Overheat = false;
                        }
                        break;
                    case false:
                        shootTime += Time.deltaTime;
                        if (shootTime >= OverheatTime)
                        {
                            shootTime = OverheatTime;
                            PSN.Overheat = true;
                        }
                        break;

                    default:
                        break;
                }
                break;
            case false:
                switch (PSN.Overheat)
                {
                    case true:
                        shootTime -= Time.deltaTime;
                        if (shootTime <= 0)
                        {
                            shootTime = 0;
                            PSN.Overheat = false;
                        }
                        break;
                    case false:
                        shootTime -= Time.deltaTime;
                        if (shootTime <= 0)
                        {
                            shootTime = 0;
                            PSN.Overheat = false;
                        }
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
}
