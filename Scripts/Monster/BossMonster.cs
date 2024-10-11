using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster : MonoBehaviour
{

    [SerializeField]
    SkinnedMeshRenderer[] m_materials;

    [SerializeField]
    public float m_health = 0f;
    [SerializeField]
    public TextMeshProUGUI m_helathText;
    [SerializeField]
    public Image bossHealthImage;

    bool m_sliderLeftHand = false;
    bool m_sliderRightHand = false;
    bool m_idle = false;
    bool m_groggy = false;
    bool m_fireball = false;

    [SerializeField]
    float m_idleCurrentTime = 0;
    float m_idleMaxTime = 3f;
    [SerializeField]
    float m_pattenDelayMaxTime = 3f; //사전 딜레이 타임
    float m_pattenCurrentDelayTime = 0;

    [SerializeField]
    float m_pattenGroggy = 0f; //그로기 타임
    float m_pattenGroggyMaxTime = 5f;


    [SerializeField]
    bool m_pattenUseActive = false;
    [SerializeField]
    float m_pattenUseTime = 0f;
    float m_pattenCurrentUseTime = 10f;

    [SerializeField]
    bool m_pattenIng = false;


    [SerializeField]
    GameObject m_pattenBackRightEffect;
    [SerializeField]
    GameObject m_pattenBackLeftEffect;
    [SerializeField]
    GameObject m_pattenBackEffect;
    [SerializeField]
    GameObject m_pattenFireballEffect;

    [SerializeField]
    GameObject m_pattenFireballDamage;

    [SerializeField]
    PlayerControll m_playerControll;

    [SerializeField]
    Transform m_fireballSpawn;

    [SerializeField]
    Animator m_animator;


    [SerializeField]
    public SubBossHealth[] m_subBossScirpts;

    bool isDamageActive = false; //보스 데미지 들어가는 변수


    [SerializeField]
    eBossStatus m_status = eBossStatus.IDLE;
    enum eBossStatus
    {
        RIGHTHAND,
        LEFTHAND,
        FIREBALL,
        GROGGY,
        IDLE,
    }

    void resetDamageEffect()
    {
        foreach (SkinnedMeshRenderer mat in m_materials)
        {
            mat.material.color = new Color(1, 1, 1);
        }

    }


    public void setDamage()
    {
        if (m_health <= 0)
        {
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




    void aEventPattenUse()
    {
        m_pattenUseActive = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        m_pattenBackRightEffect.SetActive(false);
        m_pattenBackLeftEffect.SetActive(false);
        m_pattenFireballEffect.SetActive(false);

        m_playerControll = GameObject.FindWithTag("Player").GetComponent<PlayerControll>();

        m_pattenFireballDamage = Resources.Load("Prefab/Bullet/FireBallDamage") as GameObject;

    }

    void healthCheck()
    {
        m_health = m_subBossScirpts[0].health + m_subBossScirpts[1].health + m_subBossScirpts[2].health + m_subBossScirpts[3].health;

        float healthPer = m_health / 600f;
        bossHealthImage.fillAmount = healthPer;

        m_helathText.text = Mathf.Floor(healthPer*100f) + "%";
    }

    void resetPattenBool()
    {
        m_sliderLeftHand = false;
        m_sliderRightHand = false;
        m_groggy = false;
        m_fireball = false;
    }
    void RandomPatten()
    {
        //랜덤 패턴 함수
        Debug.Log("패턴");
        resetPattenBool();
        eBossStatus random = (eBossStatus)UnityEngine.Random.Range((int)eBossStatus.RIGHTHAND, (int)eBossStatus.GROGGY);
        //사전 패턴 예고 로직
        if (random == eBossStatus.LEFTHAND)
        {

            m_pattenBackLeftEffect.SetActive(true);
        }
        else if (random == eBossStatus.RIGHTHAND)
        {

            m_pattenBackRightEffect.SetActive(true);
        }
        else if (random == eBossStatus.FIREBALL)
        {
        }
        m_status = random;

    }

    [SerializeField]
    float m_fireBallTime = 0f;
    float m_fireBallMaxTime = 3f;


    void logic()
    {

        //패턴 유지 중일 경우
        if(m_pattenUseActive)
        {
            m_pattenUseTime += Time.deltaTime;
            if (m_status == eBossStatus.FIREBALL)
            {

                m_fireBallTime += Time.deltaTime;
                if (m_fireBallTime >= m_fireBallMaxTime)
                {

                    var fireballBullet = Instantiate(m_pattenFireballDamage);
                    fireballBullet.transform.position = m_fireballSpawn.position;

                    BulletHit1 hit = fireballBullet.GetComponent<BulletHit1>();
                    m_fireBallTime = 0f;
                    Vector3 dir = (m_playerControll.transform.position - m_fireballSpawn.transform.position).normalized;
                    hit.SetFireBall(dir, 25f, 50, "Fireball");

                }
            }
            if (m_pattenUseTime >= m_pattenCurrentUseTime)
            {
                m_animator.SetBool("groggy", true);
                m_status = eBossStatus.GROGGY;
                m_groggy = true;
                m_pattenUseTime = 0;
                m_pattenUseActive = false;
                m_pattenIng = false;
                
                m_animator.SetBool("sliderLeftHand", false);
                m_animator.SetBool("sliderRightHand", false);
                m_animator.SetBool("fireball", false);

            }

        }
        else if(m_groggy)
        {
            m_pattenGroggy += Time.deltaTime;
            if (m_pattenGroggy >= m_pattenGroggyMaxTime)
            {
                m_pattenGroggy = 0;
                m_groggy = false;
                m_animator.SetBool("groggy", false);
                m_status = eBossStatus.IDLE;
            }
        }
        else if (m_status.Equals(eBossStatus.IDLE))
        {
            m_idleCurrentTime += Time.deltaTime;
            if(m_idleCurrentTime >= m_idleMaxTime)//로직 실행
            {
                RandomPatten();
                m_idleCurrentTime = 0;
            }
        }
        else if (!m_pattenIng)
        {
            //사전 예고 패턴이후 실제 패턴 실행
            m_pattenCurrentDelayTime += Time.deltaTime;
            if (m_pattenCurrentDelayTime >= m_pattenDelayMaxTime)//로직 실행
            {
                m_pattenCurrentDelayTime = 0;
                m_pattenIng = true;
                if (m_status == eBossStatus.LEFTHAND)
                {
                    m_pattenBackLeftEffect.SetActive(false);
                    m_animator.SetBool("sliderLeftHand", true);
                }
                else if (m_status == eBossStatus.RIGHTHAND)
                {
                    m_pattenBackRightEffect.SetActive(false);
                    m_animator.SetBool("sliderRightHand", true);
                }
                else if (m_status == eBossStatus.FIREBALL)
                {
                    m_animator.SetBool("fireball", true);
                }
                else if (m_status == eBossStatus.GROGGY)
                {
                    m_animator.SetBool("groggy", true);
                }

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        healthCheck();
        logic();
    }
}
