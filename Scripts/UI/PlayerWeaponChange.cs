using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class PlayerWeaponChange : MonoBehaviour
{

    [SerializeField]
    GameObject m_yesObj;
    [SerializeField]
    GameObject m_noObj;


    [SerializeField]
    GameObject m_selectObj;

    [SerializeField]
    GameObject[] slotWeapons = new GameObject[8];


    [SerializeField]
    GameObject weaponInventory;

    int selectWeaponNumber = 0;

    [SerializeField]
    GraphicRaycaster m_rayCaster;

    PointerEventData m_rayHit;

    [SerializeField]
    TextMeshProUGUI m_statAmountText;

    [SerializeField]
    GameObject statMenu;

    [SerializeField]
    GameObject statYes;
    [SerializeField]
    GameObject statNo;


    [SerializeField]
    TextMeshProUGUI[] m_statTexts = new TextMeshProUGUI[5];
    [SerializeField]
    Image[] m_statProgressImage = new Image[5];


    int m_trion = 100;
    
    [SerializeField]
    TextMeshProUGUI m_trionText;
    [SerializeField]
    Image m_currentTrion;
    [SerializeField]
    Image m_currentTrion_Over;




    float[] PROGRESSBAR_VAULE = new float[] { 0, 0.08f, 0.175f, 0.275f, 0.385f, 0.495f, 0.590f, 0.705f, 0.80f, 0.9f, 1f };

    int m_playerStatPoint = 20;

    int stat_maxTrion = 0;
    int stat_def = 0;
    int stat_spd = 0;
    int stat_sword = 0;
    int stat_projectile = 0;

    public void addStat(int type)
    {
        if (m_playerStatPoint <= 0)
            return;
        if(type == 0)
        {
            if (stat_maxTrion >= 10)
                return;
            stat_maxTrion++;
            m_trion += 7;
            m_statTexts[type].text = stat_maxTrion.ToString();
            m_statProgressImage[type].fillAmount = PROGRESSBAR_VAULE[stat_maxTrion];

        }
        else if (type == 1)
        {
            if (stat_def >= 10)
                return;
            stat_def++;
            m_statTexts[type].text = stat_def.ToString();
            m_statProgressImage[type].fillAmount = PROGRESSBAR_VAULE[stat_def];
        }
        else if (type == 2)
        {
            if (stat_spd >= 10)
                return;
            stat_spd++;
            m_statTexts[type].text = stat_spd.ToString();
            m_statProgressImage[type].fillAmount = PROGRESSBAR_VAULE[stat_spd];
        }
        else if (type == 3)
        {
            if (stat_sword >= 10)
                return;
            stat_sword++;
            m_statTexts[type].text = stat_sword.ToString();
            m_statProgressImage[type].fillAmount = PROGRESSBAR_VAULE[stat_sword];
        }
        else if (type == 4)
        {
            if (stat_projectile >= 10)
                return;
            stat_projectile++;
            m_statTexts[type].text = stat_projectile.ToString();
            m_statProgressImage[type].fillAmount = PROGRESSBAR_VAULE[stat_projectile];
        }


        m_playerStatPoint--;

    }
    public void subStat(int type)
    {
        if (type == 0)
        {
            if (stat_maxTrion <= 0)
                return;
            stat_maxTrion--;
            m_trion -= 7;
            m_statTexts[type].text = stat_maxTrion.ToString();
            int progress = stat_maxTrion != 0 ? stat_maxTrion : 0;

            m_statProgressImage[type].fillAmount = PROGRESSBAR_VAULE[progress];

        }
        else if (type == 1)
        {
            if (stat_def <= 0)
                return;
            stat_def--;
            m_statTexts[type].text = stat_def.ToString();
            int progress = stat_def != 0 ? stat_def : 0;
            m_statProgressImage[type].fillAmount = PROGRESSBAR_VAULE[progress];

        }
        else if (type == 2)
        {
            if (stat_spd <= 0)
                return;
            stat_spd--;
            m_statTexts[type].text = stat_spd.ToString();
            int progress = stat_spd != 0 ? stat_spd - 1 : 0;
            m_statProgressImage[type].fillAmount = PROGRESSBAR_VAULE[progress];
        }
        else if (type == 3)
        {
            if (stat_sword <= 0)
                return;
            stat_sword--;
            m_statTexts[type].text = stat_sword.ToString();
            int progress = stat_sword != 0 ? stat_sword : 0;
            m_statProgressImage[type].fillAmount = PROGRESSBAR_VAULE[progress];
        }
        else if (type == 4)
        {
            if (stat_projectile <= 0)
                return;
            stat_projectile--;
            m_statTexts[type].text = stat_projectile.ToString();
            int progress = stat_projectile != 0 ? stat_projectile : 0;
            m_statProgressImage[type].fillAmount = PROGRESSBAR_VAULE[progress];
        }

        m_playerStatPoint++;

    }

    // Start is called before the first frame update

    public void setEquieAnswerNo()
    {
        selectWeaponSlot(99);
        weaponInventory.SetActive(false);
        statMenu.SetActive(true);
    }
    public void selectWeaponSlot(int selectWeaponNumber)
    {
        int i = 0;
        foreach (var objs in slotWeapons)
        {
            if( i ==  selectWeaponNumber )
            {
                objs.gameObject.SetActive(true);
            }
            else
                objs.gameObject.SetActive(false);
            i++;
        }
        statMenu.SetActive(false);
        weaponInventory.SetActive(true);
    }
    void Start()
    {
        int i = 0;
        foreach (var objs in GetComponentsInChildren<Transform>())
        {
            if(objs.CompareTag("SelectUi"))
            {
                slotWeapons[i] = objs.gameObject;
                objs.gameObject.SetActive(false);
                i++;
            }
        }
        weaponInventory.SetActive(false);

        m_rayHit = new PointerEventData(null);
    }

    // Update is called once per frame
    void Update()
    {
        m_statAmountText.text = "Æ÷ÀÎÆ®: " + m_playerStatPoint;
        

        m_trion =  100 - 5 * PlayerSelectWeaponManager.Instance.m_weponSelectCount + stat_maxTrion * 7;
        m_trionText.text = m_trion + "%";

        m_currentTrion.fillAmount = m_trion * 0.01f;

        m_currentTrion_Over.fillAmount = Mathf.Clamp((m_trion * 0.01f - 1f), 0f, 1f);
    }
}
