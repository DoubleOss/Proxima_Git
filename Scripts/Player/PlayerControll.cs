using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static UIScrollView;

public class PlayerControll : MonoBehaviour
{

    Animator m_animator;
    Rigidbody m_rigidbody;


    [SerializeField]
    Transform m_startRay;

    [SerializeField]
    bool m_IsJumping = false;
    [SerializeField]
    bool m_IsLanding = false;


    [SerializeField]
    Transform m_shooterFirePos;
    [SerializeField]
    GameObject m_bulletPrefab;
    [SerializeField]
    GameObject m_bulletRightPrefab;

    [SerializeField]
    GameObject m_gunBulletPrefab;


    bool m_aniMoveStop = false;

    int m_mainWeaponNumber = 1; //1~4
    int m_subWeaponNumber = 5; //5~8


    WeaponManager.eWeaponType m_playerSelectWeaponType = WeaponManager.eWeaponType.SWORLD;

    CharacterController m_characterController;

    string m_playerName = "Tester";

    float m_leftMouseKeyDelayCheck = 0f;
    bool m_leftMouseKeyDelay = false;

    [SerializeField]
    GameObject m_autoShieldObj;

    [SerializeField]
    GameObject[] m_hudLeftRightSlots = new GameObject[2];

    [SerializeField]
    GameObject[] m_hudUpperYPos = new GameObject[2];

    [SerializeField]
    GameObject m_equiePistol;

    [SerializeField]
    GameObject m_equieKatana;

    [SerializeField]
    GameObject m_equieRifle;

    [SerializeField]
    Transform m_pistolFirePos;
    [SerializeField]
    Transform m_rifleFirePos;

    [SerializeField]
    public bool m_shooterMove = false;


    [SerializeField]
    SwrodHit m_swordHit;

    [SerializeField]
    GameObject m_subShield;

    CameraShake shake;

    [SerializeField]
    Image m_healthBar;
    [SerializeField]
    TextMeshProUGUI m_healthBarText;


    public float m_DieAniTime = 0f;
    public float m_DieAniMaxTime = 5f;



    public float m_trion = 1000f;

    public float m_maxTrion = 1000f;

    public string getPlayerName()
    {
        return m_playerName;
    }

    bool m_playerSprint = false;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_bulletRightPrefab = Resources.Load("Prefab/Bullet/ShooterRightBullet") as GameObject;

        m_bulletPrefab = Resources.Load("Prefab/Bullet/ShooterBullet") as GameObject;

        m_gunBulletPrefab = Resources.Load("Prefab/Bullet/GunBullet") as GameObject;

        shake = Camera.main.GetComponentInParent<CameraShake>();
        //m_autoShieldObj = GetComponentInChildren<AutoShield>().gameObject;
        m_animator.SetBool("IsKatana", true);

        m_characterController = GetComponent<CharacterController>();


        PlayerSelectWeaponManager selectManager = PlayerSelectWeaponManager.Instance;
        int i = 0;
        foreach (Transform trans in m_hudLeftRightSlots[0].GetComponentsInChildren<Transform>())
        {
            if(trans.CompareTag("PlayerWeaponSlot"))
            {
                WeaponManager.WeaponData data = selectManager.m_mainWeapon[i];
                trans.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/WeaponIcon/" + data.m_spriteType.ToString());
                i++;
            }
        }
        i = 0;
        foreach (Transform trans in m_hudLeftRightSlots[1].GetComponentsInChildren<Transform>())
        {
            if (trans.CompareTag("PlayerWeaponSlot"))
            {
                WeaponManager.WeaponData data = selectManager.m_subWeapon[i];
                trans.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/WeaponIcon/" + data.m_spriteType.ToString());
                i++;
            }
        }
        m_swordHit = GetComponentInChildren<SwrodHit>();

        m_mainWeaponNumber = 1;
        clearAnimatorLayerWeight();
        hudUpperReset(0);
        hudUpper(0);

        hudUpperReset(1);
        m_subWeaponNumber = 5;
        clearAnimatorLayerWeight();
        hudUpper(1);

        m_equieKatana.SetActive(true);
        m_animator.SetBool("IsKatana", true);

    }

    // Update is called once per frame
    void Update()
    {
        if(!isDie)
        {
            InputMovement();
            leftClickAttack();
            numberKeyWeaponChange();
            rightClickAttack();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadingManager.Instance.SetActiveLoading();
            LoadSceneManager.Instance.LoadSceneAsyc(LoadSceneManager.eSceneState.Menu);
        }

        if(m_swordHit != null)
        {
            int katanaCount = m_animator.GetInteger("KatanaAttackCount");
            if (katanaCount > 0)
            {
                m_swordHit.hitEffect = true;
            }
            else
            {
                m_swordHit.hitEffect = false;
            }
        }

        float per = m_trion / m_maxTrion;



        if (per <= 0 && !isDie)
        {
            per = 0;
            isDie = true;
            m_animator.SetTrigger("IsDie");
            m_subShield.SetActive(false);

            m_animator.SetLayerWeight(1, 0);
            m_animator.SetLayerWeight(2, 0);
        }

        if(isDie)
        {
            per = 0;
            m_DieAniTime += Time.deltaTime;
            if(m_DieAniTime >= m_DieAniMaxTime)
            {
                LoadSceneManager.Instance.LoadSceneAsyc(LoadSceneManager.eSceneState.Menu);
                LoadingManager.Instance.SetActiveLoading();
            }
        }

        m_healthBar.fillAmount = per;

        m_healthBarText.text = Mathf.Floor(per * 100f) + "%";


    }

    private void LateUpdate()
    {
        Vector3 playerRotate = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1));

        //LookRotation 물체의 Z 축이 Vector 의 방향을 바라보는 값을 반환
        // 지정된 방향을 가르기키는 Rotate 축을 생성하는 함수 보면 된다.

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * 10f);
    }


    void clearAnimatorLayerWeight()
    {
        m_animator.SetLayerWeight(1, 0);
        m_animator.SetLayerWeight(2, 0);
        m_animator.SetBool("IsKatana", false);
        m_animator.SetBool("IsPistol", false);
        m_animator.SetBool("IsRifle", false);

        m_equieKatana.SetActive(false);
        m_equiePistol.SetActive(false);
        m_equieRifle.SetActive(false);
    }

    void hudUpperReset(int mainSub)
    {
        foreach (Transform obj in m_hudLeftRightSlots[mainSub].GetComponentsInChildren<Transform>())
        {
            if (obj.gameObject.name.Contains("Slot_"))
            {
                Vector3 vec = m_hudUpperYPos[mainSub].transform.localPosition;
                Vector3 orign = obj.gameObject.transform.localPosition;
                obj.transform.localPosition = new Vector3(orign.x, vec.y, orign.z);
            }
        }

    }
    void hudUpper(int mainSub)
    {
        if (mainSub == 0)
        {
            foreach (Transform obj in m_hudLeftRightSlots[0].GetComponentsInChildren<Transform>())
            {
                if (obj.gameObject.name.Equals("Slot_" + m_mainWeaponNumber))
                {
                    Vector3 vec = m_hudUpperYPos[0].transform.localPosition;
                    Vector3 orign = obj.gameObject.transform.localPosition;
                    obj.gameObject.transform.localPosition = new Vector3(orign.x, vec.y + 40, orign.z);
                    return;
                }
            }

        }
        else
        {
            foreach (Transform obj in m_hudLeftRightSlots[1].GetComponentsInChildren<Transform>())
            {
                if (obj.gameObject.name.Equals("Slot_" + m_subWeaponNumber))
                {
                    Vector3 vec = m_hudUpperYPos[1].transform.localPosition;
                    Vector3 orign = obj.gameObject.transform.localPosition;
                    obj.gameObject.transform.localPosition = new Vector3(orign.x, vec.y + 40, orign.z);
                    return;
                }
            }

        }


    }

    void chanageWeaponType()
    {
        WeaponManager.WeaponData data = PlayerSelectWeaponManager.Instance.m_mainWeapon[m_mainWeaponNumber - 1];
        if(data.m_type == WeaponManager.eWeaponType.SWORLD)
        {
            m_animator.SetBool("IsKatana", true);
            m_equieKatana.SetActive(true);
        }
        else if(data.m_type == WeaponManager.eWeaponType.RIFLE)
        {
            m_equieRifle.SetActive(true);
            m_animator.SetLayerWeight(2, 1);//애니메이션 레이어 변경
            m_animator.SetBool("IsRifle", true);
        }
        else if (data.m_type == WeaponManager.eWeaponType.PISTOL)
        {
            m_equiePistol.SetActive(true);
            m_animator.SetLayerWeight(1, 1);//애니메이션 레이어 변경
            m_animator.SetBool("IsPistol", true);
        }
        else if(data.m_type == WeaponManager.eWeaponType.SHOOTER)
        {
            m_animator.SetBool("IsKatana", false);

        }
        else if (data.m_type == WeaponManager.eWeaponType.SHIELD)
        {
            m_animator.SetBool("IsKatana", false);

        }
    }

    void changeSubWeapon()
    {
        m_subShield.SetActive(false);
        WeaponManager.WeaponData data = PlayerSelectWeaponManager.Instance.m_subWeapon[m_subWeaponNumber - 5];
        if (data.m_type == WeaponManager.eWeaponType.SHIELD)
        {
            Shield shield = m_subShield.GetComponent<Shield>();
            if(shield.m_durability > 0)
            {
                m_subShield.SetActive(true);
            }

        }
        else if (data.m_type == WeaponManager.eWeaponType.SHOOTER)
        {

        }
    }
    // m_mainWeaponNumber 현재 사용중인 무장 번호값
    // clearAnimatorLayerWeight(); 애니메이션 레이어 초기화
    // hudUpperReset(0); HUD 사용중인 무장 슬롯 초기화 0일경우 왼쪽 1일 경우 오른쪽
    // hudUpper(0); 현재 사용중인 무장 슬롯으로 변경 0일경우 왼쪽 1일경우 오른쪽
    // chanageWeaponType(); 각 무장에 맞는 애니메이션 레이어 변경
    void numberKeyWeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //1번 키값
        {
            
            m_mainWeaponNumber = 1;
            clearAnimatorLayerWeight();
            hudUpperReset(0);
            hudUpper(0);
            chanageWeaponType();

        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)) //2번 키값
        {
            m_mainWeaponNumber = 2;
            hudUpperReset(0);
            clearAnimatorLayerWeight();
            hudUpper(0);
            chanageWeaponType();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) //3번 키값
        {
            m_mainWeaponNumber = 3;
            hudUpperReset(0);
            clearAnimatorLayerWeight();
            hudUpper(0);
            chanageWeaponType();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) //4번 키값
        {
            m_mainWeaponNumber = 4;
            hudUpperReset(0);
            clearAnimatorLayerWeight();
            hudUpper(0);
            chanageWeaponType();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) //5번 키값
        {
            hudUpperReset(1);
            m_subWeaponNumber = 5;
            //clearAnimatorLayerWeight();
            hudUpper(1);
            changeSubWeapon();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha6)) //6번 키값
        {
            hudUpperReset(1);
            m_subWeaponNumber = 6;
            //clearAnimatorLayerWeight();
            hudUpper(1);
            changeSubWeapon();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha7)) //7번 키값
        {
            hudUpperReset(1);
            m_subWeaponNumber = 7;
            //clearAnimatorLayerWeight();
            hudUpper(1);
            changeSubWeapon();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha8)) //8번 키값
        {
            hudUpperReset(1);
            m_subWeaponNumber = 8;
            //clearAnimatorLayerWeight();
            hudUpper(1);
            changeSubWeapon();

        }

    }

    void Jump()
    {
        isJump = true;
        m_IsJumping = true;
        m_animator.SetBool("IsJump", isJump);
        m_animator.SetBool("IsJumping", m_IsJumping);
        //m_rigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }

    void delayLeftKeyActive()
    {
        m_leftMouseKeyDelay = false;
    }

    void delayShooterLeftClick()
    {
        m_shooterMove = false;
        m_shooterBulletOn = false;
        m_leftMouseKeyDelay = false;
    }
    public bool m_shooterRightMove = false;
    public bool m_shooterRightBulletOn = false;
    public bool m_rightMouseKeyDelay = false;

    public AudioClip LandingAudioClip;
    public AudioClip[] FootstepAudioClips;
    [Range(0, 1)] public float FootstepAudioVolume = 0.5f;
    public AudioClip[] katanaSfx;

    [SerializeField]
    AudioClip shoot2;
    [SerializeField]
    AudioClip GunSound;

    private void OnGun()
    {

        AudioSource.PlayClipAtPoint(shoot2, transform.position, 0.35f);
    }
    private void OnShoot()
    {
        if (FootstepAudioClips.Length > 0)
        {
            AudioSource.PlayClipAtPoint(shoot2, transform.position, FootstepAudioVolume);
        }

    }
    private void OnKatana(int katanaNumber)
    {
        if (FootstepAudioClips.Length > 0)
        {
            AudioSource.PlayClipAtPoint(katanaSfx[katanaNumber], transform.position, FootstepAudioVolume);
        }

    }
    private void OnFootStep()
    {

        if (FootstepAudioClips.Length > 0)
        {
            var index = UnityEngine.Random.Range(0, FootstepAudioClips.Length);
            AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.position, FootstepAudioVolume);
        }
        
    }

    void aLandingSound()
    {
        AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(transform.position), FootstepAudioVolume);
    }


    void delayRightKeyActive()
    {
        m_rightMouseKeyDelay = false;
    }
    void delayRightShooterClick()
    {
        m_shooterRightMove = false;
        m_shooterRightBulletOn = false;
        m_rightMouseKeyDelay = false;
    }
    void rightClickAttack()
    {
        if (Input.GetMouseButton(1))
        {
            if (m_shooterRightMove && m_shooterRightBulletOn)
                return;
            if (!m_rightMouseKeyDelay)
            {
                m_rightMouseKeyDelay = true;

                WeaponManager.WeaponData data = PlayerSelectWeaponManager.Instance.m_subWeapon[m_subWeaponNumber - 5];

                float defualtKeyInput = 0.5f;
                if (!m_shooterRightMove && m_shooterRightBulletOn)
                {
                    m_shooterRightMove = true;
                    OnShoot();

                    if (IsInvoking("delayRightShooterClick"))
                    {
                        CancelInvoke("delayRightShooterClick");
                    }
                    Invoke("delayRightShooterClick", 2f);
                    return;
                }

                else if (data.m_type == WeaponManager.eWeaponType.SHOOTER)
                {
                    ShooterAttack(false);
                }
                else if (data.m_type == WeaponManager.eWeaponType.SHIELD)
                {
                    m_subShield.SetActive(true);
                    m_subShield.GetComponent<Shield>().m_durability = 100;
                }
                m_animator.SetTrigger("IsAttack");
                if (IsInvoking("delayRightKeyActive"))
                {
                    CancelInvoke("delayRightKeyActive");
                }
                Invoke("delayRightKeyActive", defualtKeyInput);

                m_trion -= data.requestHealth;

            }
        }
    }
    void leftClickAttack()
    {
        if(Input.GetMouseButton(0))
        {
            if (m_shooterMove && m_shooterBulletOn)
                return;
            if(!m_leftMouseKeyDelay)
            {
                m_leftMouseKeyDelay = true;

                WeaponManager.WeaponData data = PlayerSelectWeaponManager.Instance.m_mainWeapon[m_mainWeaponNumber - 1];

                float defualtKeyInput = 0.5f;
                if(!m_shooterMove && m_shooterBulletOn)
                {
                    m_shooterMove = true;
                    OnShoot();
                    if (IsInvoking("delayShooterLeftClick"))
                    {
                        CancelInvoke("delayShooterLeftClick");
                    }
                    Invoke("delayShooterLeftClick", 2f);
                    return;
                }
                if (data.m_type == WeaponManager.eWeaponType.SWORLD)
                {
                    
                    katanaAttack();
                }
                else if (data.m_type == WeaponManager.eWeaponType.SHOOTER)
                {
                    ShooterAttack(true);
                }
                else if (data.m_type == WeaponManager.eWeaponType.PISTOL)
                {
                    GunAttack();
                    OnGun();
                    defualtKeyInput = data.m_delay;
                }
                else if (data.m_type == WeaponManager.eWeaponType.RIFLE)
                {
                    GunAttack();
                    OnGun();
                    defualtKeyInput = data.m_delay;
                }
                m_animator.SetTrigger("IsAttack");
                if (IsInvoking("delayLeftKeyActive"))
                {
                    CancelInvoke("delayLeftKeyActive");
                }
                Invoke("delayLeftKeyActive", defualtKeyInput);

                m_trion -= data.requestHealth;

            }
        }
    }
    void EndKatanaAtk()
    {
        m_animator.SetInteger("KatanaAttackCount", 0);
        m_aniMoveStop = false;
    }

    void aEventMoveStop()
    {
        m_aniMoveStop = true;
    }
    void katanaAttack()
    {
        int katanaCount = m_animator.GetInteger("KatanaAttackCount");
        m_animator.SetInteger("KatanaAttackCount", katanaCount+1);
        m_swordHit.hitEffect = true;
    }

    void GunAttack()
    {

        WeaponManager.WeaponData data = PlayerSelectWeaponManager.Instance.m_mainWeapon[m_mainWeaponNumber - 1];
        Vector3 vec = data.m_type == WeaponManager.eWeaponType.PISTOL ? m_pistolFirePos.position : m_rifleFirePos.position; 
        var obj = Instantiate(m_gunBulletPrefab);

        obj.transform.position = vec;

        Vector3 dir = Camera.main.ScreenPointToRay(Input.mousePosition).direction;

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 총알이 날아서 갈 목적지

        obj.GetComponent<BulletHit>().SetBullet(dir, 20f, 1f, m_playerName);

    }
    void ShieldAttack()
    {

    }

    public bool m_shooterBulletOn = false;

    

    void ShooterAttack(bool leftClick)
    {
        if(leftClick)
        {
            m_shooterMove = false;
            m_shooterBulletOn = true;
        }
        else
        {
            m_shooterRightMove = false;
            m_shooterRightBulletOn = true;
        }

        GameObject obj;
        if (!leftClick)
           obj = Instantiate(m_bulletRightPrefab);
        else
        {
            obj = Instantiate(m_bulletPrefab);
        }
        obj.transform.position = m_shooterFirePos.position;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 총알이 날아서 갈 목적지
        Bullet bullet = obj.GetComponent<Bullet>();

        bullet.SetCurveBullet(ray, 1f, 1f, leftClick, m_playerName); ; // 총알에게 목적지 + 총알 속도값 넘겨줌

    }

    void aEventlandToMove()
    {
        isJump = false;
        m_animator.SetBool("IsJump", isJump);
        m_IsLanding = false;

    }
    void aEventLandingStart()
    {
        m_IsLanding = true;
    }

    void aEventRayCheck()
    {
        rayJumpingCheck = true;
    }


    public bool rayJumpingCheck = false;

    public bool isJump = false;
    public float speed = 6f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [SerializeField]
    private Vector3 velocity;
    [SerializeField]
    private bool isGrounded;

    [SerializeField]
    public bool isAttack;


    public bool isDie = false;

    void aEventAttackStart()
    {
        isAttack = true;
    }

    void aEventAttackEnd()
    {
        isAttack = false;
    }


    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    void InputMovement()
    {
        if (isDie)
            return;
        if (m_aniMoveStop)
            return;

        //뛰기 기능
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_playerSprint = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_playerSprint = false;
        }

        //애니메이션 스프린트 블렌드 값
        float sprintAnimCurr = m_playerSprint ? 1f : 0.5f;


        isGrounded = Physics.CheckSphere(m_startRay.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 땅에 닿아있다면 y 속도를 리셋 (약간의 다운포스)
        }

        // 입력에 따라 XZ 평면에서 이동

        float x = Input.GetAxis("Horizontal") * sprintAnimCurr;
        float z = Input.GetAxis("Vertical") * sprintAnimCurr;


        //애니메이션 블렌드 트리값
        m_animator.SetFloat("y", z);
        m_animator.SetFloat("x", x);

        Vector3 move = transform.right * x + transform.forward * z;
        m_characterController.Move(move * speed * Time.deltaTime);

        // 점프 입력
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !rayJumpingCheck)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // 점프 속도 계산
            Jump();
        }

        if (!isGrounded && rayJumpingCheck)
        {
            bool check = Physics.Raycast(m_startRay.position, Vector3.down, 0.6f);
            Debug.DrawRay(m_startRay.position, Vector3.down, Color.red, 0.6f);
            if (check)
            {
                isJump = true;
                m_IsJumping = false;
                rayJumpingCheck = false;
                m_animator.SetBool("IsJumping", m_IsJumping);
                m_animator.SetBool("IsJump", isJump);

            }
        }
        m_animator.SetBool("IsJumping", !isGrounded);
        m_animator.SetBool("IsJump", !isGrounded);

        if (!isJump)
            rayJumpingCheck = false;

        // 중력 적용
        velocity.y += gravity * Time.deltaTime;

        // y 방향으로 캐릭터 이동 (중력 및 점프 반영)
        m_characterController.Move(velocity * Time.deltaTime);



        /*
        Debug.DrawRay(m_startRay.position, Vector3.down * 0.5f);
        
        if (!m_isGround)
        {
            bool check = Physics.Raycast(m_startRay.position, Vector3.down, 0.5f);

            if (check)
            {
                m_animator.SetBool("IsJumping", false);
                m_animator.SetBool("IsJump", true);
            }
        }



        m_isGround = Physics.Raycast(m_startRay.position, Vector3.down, 2f);
        //m_animator.SetBool("IsJump", !m_isGround);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_playerSprint = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_playerSprint = false;
        }


        //Vector3 moveDir = Vector3.zero;

        Vector3 forward = transform.TransformDirection(Vector3.forward);

        Vector3 right = transform.TransformDirection(Vector3.right);


        moveDir = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");

        //moveDir.y += Physics.gravity.y * Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Space) && m_isGround && ! m_IsLanding)
        {
            moveDir.y += 50 * Time.deltaTime;
            Jump();
            m_isGround = false;


        }


        moveDir.y -= Physics.gravity.magnitude * Time.deltaTime;

        float sprintAnimCurr = m_playerSprint ? 1f : 0.5f;


        m_animator.SetFloat("y", Input.GetAxisRaw("Vertical") * sprintAnimCurr);
        m_animator.SetFloat("x", Input.GetAxisRaw("Horizontal") * sprintAnimCurr);


        //m_rigidbody.MovePosition(moveDir * sprintAnimCurr * Time.deltaTime * 2f);
        //m_rigidbody.velocity = moveDir * 5f;

        moveDir = new Vector3(moveDir.x * sprintAnimCurr, moveDir.y, moveDir.z * sprintAnimCurr);
        m_characterController.Move(moveDir * Time.deltaTime * 5f);



         */


    }
}

