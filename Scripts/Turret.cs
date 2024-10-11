using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrent : MonoBehaviour
{
    GameObject m_player;
    [SerializeField]
    Transform m_firePos;

    [SerializeField]
    GameObject m_bullet;

    float m_tick = 0;
    float m_maxTick = 0;


    float m_speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindWithTag("Player");
        m_bullet = Resources.Load("Prefab/Bullet/TurretBullet") as GameObject;
        m_maxTick = UnityEngine.Random.Range(7, 12);
    }

    // Update is called once per frame
    void Update()
    {
        m_tick += Time.deltaTime;
        if( m_tick > m_maxTick)
        {
            m_tick = 0;
            var obj = Instantiate(m_bullet) as GameObject;
            obj.transform.position = m_firePos.position;
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            BulletHit1 bullet = obj.GetComponent<BulletHit1>();
            Vector3 dir = (m_player.transform.position + Vector3.up - m_firePos.transform.position).normalized;

            float damage = 20f;
            bullet.SetBullet(dir, 20f, damage, gameObject.name);


        }
    }
}
