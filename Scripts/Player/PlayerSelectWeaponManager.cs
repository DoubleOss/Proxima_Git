using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponManager;

public class PlayerSelectWeaponManager : DontDestroy<PlayerSelectWeaponManager>
{


    public WeaponData[] m_mainWeapon = new WeaponData[4];

    public WeaponData[] m_subWeapon = new WeaponData[4];

    [SerializeField]
    public int m_weponSelectCount = 0;

    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_mainWeapon[0] = WeaponManager.Instance.GetWeaponData(1).Value;//카타나
        m_mainWeapon[1] = WeaponManager.Instance.GetWeaponData(0).Value;//라이플
        m_mainWeapon[2] = WeaponManager.Instance.GetWeaponData(3).Value;//슈터
        m_mainWeapon[3] = WeaponManager.Instance.GetWeaponData(2).Value;//실드



        m_subWeapon[0] = WeaponManager.Instance.GetWeaponData(6).Value;//실드
        m_subWeapon[1] = WeaponManager.Instance.GetWeaponData(3).Value;//슈터
        m_subWeapon[2] = WeaponManager.Instance.GetWeaponData(99).Value;//NONE
        m_subWeapon[3] = WeaponManager.Instance.GetWeaponData(99).Value;//NONE

        int count = 0;
        foreach(WeaponData data in m_mainWeapon)
        {
            if(data.m_spriteType != eSpriteType.NONE)
            {
                count++;
            }
        }
        foreach (WeaponData data in m_subWeapon)
        {
            if (data.m_spriteType != eSpriteType.NONE)
            {
                count++;
            }
        }

        m_weponSelectCount = count;


    }

    // Update is called once per frame
    void Update()
    {

    }
}
