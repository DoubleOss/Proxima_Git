using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{


    float m_health = 100f;

    [SerializeField]
    Image m_healthBar;

    [SerializeField]
    SkinnedMeshRenderer[] m_materials;

    [SerializeField]
    AudioClip m_dieAudio;
    
    Animator m_animator;

    PlayerControll m_player;
    GameObject m_target;


    bool m_dieSound = false;
    bool m_dieAni = false;

    float m_tick = 0f;
    float m_maxTick = 3f;

    void aEventSetDieDestory()
    {
        m_dieAni = true;
    }


    void resetDamageEffect()
    {
        foreach (SkinnedMeshRenderer mat in m_materials)
        {
            mat.material.color = new Color(1, 1, 1);
        }

    }

    private void OnDie()
    {

         AudioSource.PlayClipAtPoint(m_dieAudio, transform.position, 0.5f);

    }

    public enum eState
    {
        Idle,
        Move,
        Attack,
        Damaged
    }

    [SerializeField]
    eState m_state;
    bool m_isMove = false;
    NavMeshAgent m_agent;
    float m_attackArea = 5f;
    [SerializeField]
    float m_sight = 10f;

    public void SetState(eState state)
    {
        m_state = state;
    }

    bool FindTarget()
    {
        int checkLevel = 0;
        // 1. 거리 체크
        var dist = Vector3.Distance(transform.position, m_player.transform.position);
        if (Util.isEqual(dist, m_sight) || dist < m_sight)
        {
            checkLevel++;
        }
        //2. 장애물 체크
        Vector3 centerPos = transform.position + Vector3.up * 1.0f;
        var dir = ((m_player.transform.position + Vector3.up * 1.0f) - centerPos).normalized;
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(centerPos, dir, out hit, m_sight, -1 & (~(1 << LayerMask.NameToLayer("AttackArea")))))
        {
            if (hit.collider.CompareTag("Player"))
                checkLevel++;
        }
        //3. 시야각 체크
        /*  var rad = Vector3.Dot(transform.forward, dir);
          if(rad > 0f)
          {
              checkLevel++;
          }*/
        if (checkLevel == 2)
            return true;
        return false;
    }
    void TargetChasing()
    {
        m_target = m_player.gameObject;
        if (!m_isMove)
        {
            m_isMove = true;
            m_animator.SetBool("run", m_isMove);
            
            m_agent.stoppingDistance = m_attackArea;
        }
        if (m_target)
        {
            m_agent.SetDestination(m_target.transform.position);
        }
        var dist = Vector3.Distance(transform.position, m_target.transform.position);
        if (Util.isEqual(dist, m_agent.stoppingDistance) || dist < m_agent.stoppingDistance)
        {
            SetState(eState.Idle);
            m_animator.SetBool("run", false);
            m_isMove = false;
        }
    }


    public void setDamage(float damage)
    {
        m_health -= damage;
        if (m_health <= 0 && !m_dieSound)
        {
            m_dieSound = true;
            OnDie();
            m_animator.SetBool("Die", true);
        }

        foreach (SkinnedMeshRenderer mat in m_materials)
        {
            mat.material.color = Color.red;
        }

        if (IsInvoking("resetDamageEffect"))
        {
            CancelInvoke("resetDamageEffect");
        }
        Invoke("resetDamageEffect", 0.2f);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_agent = GetComponent<NavMeshAgent>();
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>();

    }

    // Update is called once per frame
    void Update()
    {
        m_healthBar.fillAmount = m_health * 0.01f;

        if(m_dieAni)
        {
            m_tick += Time.deltaTime;
            if (m_tick >= m_maxTick)
            {
                Destroy(gameObject);
            }
        }

    }

    float m_checkIdleTick = 0;

    void Ai()
    {
        /*

        switch (m_state)
        {
            case eState.Idle:
                m_checkIdleTick += Time.deltaTime;
                if (m_checkIdleTick >= m_IdleTime)
                {
                    if (m_target != null)
                    {
                        var dist = Vector3.Distance(transform.position, m_target.transform.position);
                        if (Util.isEqual(dist, m_navAgent.stoppingDistance) || dist < m_navAgent.stoppingDistance)
                        {
                            SetState(eState.Attack);
                            ResetMove();
                            transform.forward = (m_target.transform.position - transform.position).normalized;
                            m_animCtr.Play(MonsterAnimController.eAnimState.attack1);
                            return;
                        }
                    }
                    SetState(eState.Move);
                }
                break;
            case eState.Move:
                if (FindTarget())
                {
                    TargetChasing();
                }
                else
                {
                    RoamingMove();
                }
                break;
            case eState.Attack:
                break;
            case eState.Damaged:
                break;
        }         * 
         * 
         */

    }
}
