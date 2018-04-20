using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingNew : MonoBehaviour {
	public float fireRate = 1f;
	public int damage = 5;

	// public bool beam;
	public GameObject projectile;
	public List<GameObject> projectileSpawns;

    public AimScale AS;

    AudioSource audioSource;

    // public float OverheatTime = 2f;

    [HideInInspector]
    public float ShootTime = 0;

	List<GameObject> m_lastProjectiles = new List<GameObject> ();
	float m_fireTimer = 0.0f;

    PlayerShootingNew PSN;

    [HideInInspector]
    public bool shoot;

    [HideInInspector]
    public bool Overheat = false;

    [HideInInspector]
    GunTime GT;

    //public bool scaleOnMoving;

    //public bool scaleOnShooting;

    float radius = 5;

    float k = 2;

    Vector3 overHeatVec;

    Vector3 moveVec;

    // Use this for initialization
    void Start () {
        PSN = GetComponent<PlayerShootingNew>();
        GT = GetComponent<GunTime>();
        audioSource = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () {

        if (Overheat == false)
        {
            shoot = true;
            m_fireTimer += Time.deltaTime;

            if (m_fireTimer >= fireRate)
            {
                audioSource.Play();
                if (AS.overheatScale + AS.overheatScaleUp * Time.deltaTime > AS.overheatScaleLimit)
                {
                    AS.overheatScale = AS.overheatScaleLimit;
                }
                else
                {
                    AS.overheatScale += AS.overheatScaleUp * Time.deltaTime;
                }
                SpawnProjectiles();
                m_fireTimer = 0f;
            }
        }

        else if(Overheat == true)
        {
            //if(AS.overheatScale - AS.overheatScaleDown * Time.deltaTime < 0)
            //{
            //    AS.overheatScale = 0;
            //}
            //else
            //{
            //    AS.overheatScale -= AS.overheatScaleDown * Time.deltaTime;
            //}

            shoot = false;
            m_fireTimer = 0f;
        }

        //if(shoot == false)
        //{
        //    if (AS.overheatScale - AS.overheatScaleDown * Time.deltaTime < 0)
        //    {
        //        AS.overheatScale = 0;
        //    }
        //    else
        //    {
        //        AS.overheatScale -= AS.overheatScaleDown * Time.deltaTime;
        //    }
        //}
        
    }

	void SpawnProjectiles(){
		if(!projectile){
			return;
		}

		for(int i = 0; i < projectileSpawns.Count; i++){
			if(projectileSpawns[i]){
				GameObject proj = Instantiate (projectile, projectileSpawns [i].transform.position,
					Quaternion.Euler (projectileSpawns [i].transform.forward)) as GameObject;

				Vector3 oldScale = proj.transform.localScale;

				Vector3 localScale = transform.localScale;
				Vector3 lossyScale = transform.lossyScale;

				Vector3 scaleFactor = new Vector3(lossyScale.x / localScale.x,
					lossyScale.y / localScale.y, lossyScale.z / localScale.z);


				Vector3 newScale = new Vector3(oldScale.x * scaleFactor.x,
					oldScale.y * scaleFactor.y, oldScale.z * scaleFactor.z);
				proj.transform.localScale = newScale;


                proj.transform.forward = projectileSpawns[i].transform.forward;
                //Vector3 overHeatVec = new Vector3(RandomSpreadCircle(GT.overheatRate/100).x, RandomSpreadCircle(GT.overheatRate/100).y, 0f);                 

                if (AS!= null && AS.scaleOnMoving)
                {
                    //Debug.Log(AS.aimScale * k);
                    moveVec = RandomSpreadCircle(AS.aimScale * k);
                }
                else
                {
                    moveVec = new Vector3(0f, 0f, 0f);
                }

                Vector3 shiftGlobal = proj.transform.TransformVector(moveVec);
                proj.transform.forward += shiftGlobal;

				proj.GetComponent<BaseProjectile> ()
					.FireProjectile (projectileSpawns [i], null,
						proj.transform.forward, damage);

				m_lastProjectiles.Add (proj);
			}
		}
	}

    Vector2 RandomSpreadCircle(float spreadRadius)
    {

        Vector2 randomCircle = new Vector2();

        randomCircle = Random.insideUnitCircle * spreadRadius;

        return randomCircle;
    }


}
