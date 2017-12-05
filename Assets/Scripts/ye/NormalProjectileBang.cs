using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectileBang : BaseProjectile {

    public float m_speed = 5.0f;
    public float m_destroyDistance = 3.0f;

    protected Vector3 m_direction;
    protected bool m_fired;

    protected GameObject m_launcher;

    protected Vector3 m_initPos;

    public GameObject respawnProjectile;

    GameObject target;

    public int respawnDamage;

    public int respawnNum;

    GameObject instantiateProjectile;
    GameObject player;

    protected virtual void Start()
    {
        player = GameObject.Find("PlayerHitCube");
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (m_fired)
        {
            transform.position += m_direction * m_speed * Time.deltaTime;
            
            
        }

        if (isOutOfDistance()|| isTriggered())
        {
            //Debug.Log("out of range");

            //BreakToSmall(respawnProjectile, respawnNum);

            BreakToSmall(respawnProjectile, respawnNum);
            //Destroy(this.gameObject);

        }

    }

    public override void FireProjectile(GameObject launcher, GameObject target, Vector3 direction, int damage)
    {
        m_damage = damage;
        if (launcher)
        {
            m_launcher = launcher;
            if (direction != Vector3.zero)
            {
                m_direction = direction;
            }
            else
            {
                m_direction = (target.transform.position - launcher.transform.position).normalized;
            }
            m_fired = true;
        }
        m_initPos = transform.position;
    }

    public override void FireProjectileNoDirection(GameObject launcher, Vector3 direction, int damage)
    {
        m_damage = damage;
        if (launcher)
        {
            m_launcher = launcher;

            m_direction = direction;

            m_fired = true;
        }
        m_initPos = transform.position;
    }

    protected virtual void OnCollisionEnter(Collision coll)
    {
        //Debug.Log(coll.transform);
        HealthSystem hs = coll.transform.GetComponentInParent<HealthSystem>();
        if (hs)
        {
            hs.DoDamage(m_damage, gameObject, coll);
        }
        Destroy(this.gameObject);
    }

    protected bool isOutOfDistance()
    {
        return Vector3.Distance(m_initPos, transform.position) > m_destroyDistance;
    }

    bool isTriggered()
    {
        //GameObject player=GameObject.Find("PlayerHitCube");

        if (player!=null)
        {
            Vector3 relativeDistance = player.transform.position - this.transform.position;
            Debug.Log("relative angle:" + Vector3.Angle(relativeDistance, this.transform.forward));
            if (Vector3.Angle(relativeDistance, this.transform.forward) >= 75)
            {
                return true;
            }
        }
        else { Debug.Log("not found object!!!"); }
        return false;
    }

    void BreakToSmall(GameObject respawnProjectile, float num)
    {
        
        //Debug.Log("break " + gameObject.transform);
        for(int i = 0; i< num; i++)
        {
            //Debug.Log(i);
            instantiateProjectile = Instantiate(respawnProjectile, gameObject.transform.position, Quaternion.Euler(gameObject.transform.forward)) as GameObject;
            Vector3 localDir = new Vector3(BreakVectorCircle(i, num).x, BreakVectorCircle(i, num).y, 0);
            Vector3 globalDir = transform.TransformDirection(localDir);
            instantiateProjectile.GetComponent<BaseProjectile>()
                    .FireProjectileNoDirection(gameObject, globalDir, respawnDamage);
        }
        //Debug.Log("before destroy");
        Destroy(this.gameObject);
        //Debug.Log("after destroy");
    }

    Vector2 BreakVectorCircle(int i, float num)
    {
        
        Vector2 breakVector = new Vector2();
        //Debug.Log(i * 360 / (float)(num) + " " + i * 360 / (float)(num));
        breakVector.x = Mathf.Sin(i * Mathf.PI * 2 / num );
        breakVector.y = Mathf.Cos(i * Mathf.PI * 2 / num );
        //Debug.Log(i + " " + breakVector);
        return breakVector;
    }


}
