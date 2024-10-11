using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    // Start is called before the first frame update
    float m_speed = 1f;

    Vector3 m_dir = Vector3.zero;

    bool isCurve = false;

    Vector3 startPoint;
    Vector3 endPoint;
    Vector3 controlPoint1;
    Vector3 controlPoint2;



    string m_thrower = "";

    float m_damage = 1f;



    float length = 0;

    float t = 0f;

    bool m_isPlay = false;

    float m_removeMaxTimer = 10f;
    float m_removecurrentTimer = 0f;


    [SerializeField]
    GameObject m_effect1;
    [SerializeField]
    GameObject m_effect2;

    bool isMove = false;

    [SerializeField]
    bool isAutoMove = false;
    float m_autoTick = 0f;
    float m_autoMaxTick = 6f;
    [SerializeField]
    bool left = false;

    [SerializeField]
    GameObject m_sfx;

    PlayerControll m_playerControll;
    public string getThrower()
    {
        return m_thrower;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Bullet") && ! other.CompareTag("AutoShieldObj"))
        {
            if (other.CompareTag("Shield"))
            {
               // PlayerControll script = gameObject.GetComponentInParent<PlayerControll>();
                //string ownerName = script.getPlayerName();

                Shield shield = other.GetComponent<Shield>();
                


                if (m_thrower.Equals(shield.m_PlayerControll.getPlayerName()))
                    return;

                Destroy(gameObject);

                //float requestHealth = script.getPlayerSelectWeapon().m_weaponData.requestHealth;
                //script.decreasePlayerHealth(requestHealth);

                //if (script.getPlayerHealth() - requestHealth <= 0)
                //{
                   // script.aEvent_BackChangeWeapon();
                //}
            }
            if(other.gameObject.CompareTag("Monster"))
            {
                Monster mon = other.GetComponent<Monster>();
                mon.setDamage(m_damage);
            }
            if(other.gameObject.CompareTag("HitBox"))
            {
                var subBoss = other.GetComponent<SubBossHealth>();
                subBoss.health -= m_damage;
                var bossMonster = other.GetComponentInParent<BossMonster>();
                bossMonster.setDamage();


            }
            setHitEffect();


        }
        else if (other.CompareTag("Player"))
        {
            setHitEffect();
            //PlayerControll playerControll = other.GetComponent<PlayerControll>();
            //playerControll.decreasePlayerHealth(m_damage);
            //playerControll.setPlayerBackStep();
        }

    }

    void setHitEffect()
    {
       
        var effect1 = Instantiate(m_effect1);
        var effect2 = Instantiate(m_effect2);

        var sfx2 = Instantiate(m_sfx);

        effect1.transform.position = transform.position;
        effect2.transform.position = transform.position;

        sfx2.transform.position = transform.position;

        Quaternion look;

        if (!isCurve)
        {
            look = Quaternion.LookRotation(transform.position);
        }
        else
        {
            look = Quaternion.LookRotation(endPoint);
        }
        effect1.transform.rotation = look;
        effect2.transform.rotation = look;

        Destroy(gameObject);
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = uuu * p0; // (1-t)^3 * p0
        point += 3 * uu * t * p1; // 3 * (1-t)^2 * t * p1
        point += 3 * u * tt * p2; // 3 * (1-t) * t^2 * p2
        point += ttt * p3; // t^3 * p3

        return point;
    }

    public void SetBullet(Vector3 dir, float speed, float damage, string thrower = "NULL")
    {
        m_dir = dir;
        m_speed = speed;
        m_thrower = thrower;
        m_damage = damage;
    }

    public void SetCurveBullet(Ray ray, float speed, float damage, bool left, string thrower = "NULL")
    {
        m_playerControll = GameObject.FindWithTag("Player").GetComponent<PlayerControll>();
        m_damage = damage;
        m_speed = speed;
        isCurve = true;
        m_thrower = thrower;
        this.left = left;

        startPoint = transform.position;
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("Ignore Raycast"));


        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50f, layerMask))
        {
            endPoint = hit.point;
        }
        else
        {
            endPoint = ray.GetPoint(50);
        }

        //Vector3 random = Random.insideUnitSphere * 1.25f;

        //endPoint = new Vector3(random.x + endPoint.x, random.y + endPoint.y, random.z + endPoint.z);

        controlPoint1 = startPoint + Vector3.left + Vector3.right * (endPoint.x - startPoint.x) * 0.5f;
        controlPoint2 = endPoint + Vector3.left + Vector3.right * (endPoint.x - startPoint.x) * 0.5f;

    }

    // Start is called before the first frame update
    void Start()
    {
        m_effect1 = Resources.Load("Prefab/Effect/FX_Attack01_01_Big") as GameObject;
        m_effect2 = Resources.Load("Prefab/Effect/FX_Attack01_01_dust") as GameObject;

        m_sfx = Resources.Load("Prefab/Explosion") as GameObject; 

    }


    // Update is called once per frame
    void Update()
    {
        if (isCurve)
        {
            if(left)
            {
                if (!m_playerControll.m_shooterMove && !isAutoMove)
                {
                    m_autoTick += Time.deltaTime;
                    if (m_autoTick >= m_autoMaxTick)
                    {
                        isAutoMove = true;
                    }
                    return;
                }
                else
                {

                }
            }
            else
            {
                if (!m_playerControll.m_shooterRightMove && !isAutoMove)
                {
                    m_autoTick += Time.deltaTime;
                    if (m_autoTick >= m_autoMaxTick)
                    {
                        isAutoMove = true;
                    }
                    return;
                }
                else
                {

                }
            }

        }


        if (isCurve)
        {
            t += m_speed * Time.deltaTime;

            if (t > 1f)
            {
                
                var effect1 = Instantiate(m_effect1);
                var effect2 = Instantiate(m_effect2);

                effect1.transform.position = transform.position;
                effect2.transform.position = transform.position;

                Quaternion look = Quaternion.LookRotation(endPoint);
                effect1.transform.rotation = look;
                effect2.transform.rotation = look;

                Debug.Log("여기 걍 데미지 안들어가고 펑");
                Destroy(gameObject);
            }

            Vector3 position = CalculateBezierPoint(t, startPoint, controlPoint1, controlPoint2, endPoint);
            transform.position = position;

        }
        else
        {
            transform.position += m_dir * m_speed * Time.deltaTime;
        }

        m_removecurrentTimer += Time.deltaTime;
        if(m_removecurrentTimer > m_removeMaxTimer)
        {
            Destroy(gameObject);
        }
    }
}
