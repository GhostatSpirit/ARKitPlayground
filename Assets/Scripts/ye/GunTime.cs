using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTime : MonoBehaviour {

    
    [HideInInspector]
    public float shootTime = 0f;

    public float OverheatTime = 2f;
	public float coolDownTime = 2f;

    PlayerShootingNew PSN;

    public float overheatRate;

    public AimScale AS;

	// Use this for initialization
	void Start () {
        PSN = GetComponent<PlayerShootingNew>();
	}
	
	// Update is called once per frame
	void Update () {
		float coolRate = OverheatTime / coolDownTime;
        overheatRate = shootTime / OverheatTime * 100;

        switch (PSN.enabled)
        {
            case true:
                switch (PSN.Overheat)
                {
					case true:
				
                        shootTime -= Time.deltaTime * coolRate;
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

                if (AS.overheatScale - AS.overheatScaleDown * Time.deltaTime < 0)
                {
                    AS.overheatScale = 0;
                }
                else
                {
                    AS.overheatScale -= AS.overheatScaleDown * Time.deltaTime;
                }


                switch (PSN.Overheat)
                {
                    case true:
						shootTime -= Time.deltaTime  * coolRate;
                        if (shootTime <= 0)
                        {
                            shootTime = 0;
                            PSN.Overheat = false;
                        }
                        break;
                    case false:
						shootTime -= Time.deltaTime  * coolRate;
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
