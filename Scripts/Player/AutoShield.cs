using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShield : MonoBehaviour
{

    [SerializeField]
    PlayerControll m_player;
    [SerializeField]
    GameObject m_shieldObj;
    [SerializeField]
    GameObject m_shieldObj2;


    bool m_isShieldMove = false;

    GameObject m_navBullet;

    private void OnTriggerEnter(Collider other)
    {
        if( other.CompareTag("Bullet"))
        {
            BulletHit bulletHit = other.GetComponent<BulletHit>();
            if(! bulletHit.getThrower().Equals(m_player.getPlayerName()) && ! m_isShieldMove)
            {
                m_isShieldMove = true;
                m_navBullet = other.gameObject;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        m_player = GetComponentInParent<PlayerControll>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isShieldMove)
        {
            if (m_navBullet == null)
            {
                m_isShieldMove = false;

            }
            else
            {
                var dir = m_navBullet.transform.position - m_shieldObj.transform.position;
                m_shieldObj.transform.position += dir.normalized * 10f * Time.deltaTime;

                Quaternion look = Quaternion.LookRotation(transform.position);
                m_shieldObj.transform.rotation = look;
                float distance = Vector3.Distance(m_player.transform.position, m_shieldObj.transform.position);
                if(distance > 5f)
                {
                    m_isShieldMove = false;
                    m_navBullet = null;

                }
                
            }

        }
    }
}
