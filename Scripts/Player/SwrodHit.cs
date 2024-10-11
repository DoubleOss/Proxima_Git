using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SwrodHit : MonoBehaviour
{

    GameObject m_effect;
    PlayerControll m_player;
    [SerializeField]
    BossMonster m_bossMonster;
    public bool hitEffect = true;
    private void OnTriggerEnter(Collider other)
    {
        
        if(!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Shield"))
        {
            if(m_player.isAttack)
            {
                if (other.gameObject.CompareTag("Monster"))
                {
                    Monster mon = other.gameObject.GetComponent<Monster>();
                    mon.setDamage(5);
                    if (hitEffect)
                    {
                        var effect1 = Instantiate(m_effect);
                        effect1.transform.position = transform.position;
                    }
                }
                if (other.gameObject.CompareTag("HitBox"))
                {
                    SubBossHealth mon = other.gameObject.GetComponent<SubBossHealth>();
                    mon.health -= 5;
                    m_bossMonster.setDamage();
                    if (hitEffect)
                    {
                        var effect1 = Instantiate(m_effect);
                        effect1.transform.position = transform.position;
                    }
                }

            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_player = GetComponentInParent<PlayerControll>();
        m_effect = Resources.Load("Prefab/Effect/FX_Stagger_01") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
