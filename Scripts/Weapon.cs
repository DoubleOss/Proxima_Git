using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponManager;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    public Transform m_firePos;

    public WeaponManager.WeaponData m_weaponData;

    public WeaponManager.eWeaponType m_type;
    public int m_id;
    public string m_name;
    public float m_damage;
    public int m_createBulletCount;
    public float m_speed;


    // Start is called before the first frame update
    void Start()
    {

        string name = gameObject.name; //게임오브젝트 이름 가져오기
        /*
        string[] splitStr = name.Split('_'); // 게임오브젝트 이름 _ 를 기준으로 나누기 ex) 0_gun_pistol  0 //  gun //  pistol
        string strId = splitStr[0]; // 글씨 형태의 ID 값 변수에 저장
        int id = int.Parse(strId); // 글씨 형태의 ID값 정수형으로 변환
        */


        WeaponManager manager = WeaponManager.Instance;
        //m_weaponData = manager.GetWeaponData(id).Value;
        

        if(m_weaponData.m_type.Equals(WeaponManager.eWeaponType.SWORLD))
            return;

        foreach( Transform obj in gameObject.GetComponentsInChildren<Transform>())
        {
            if(obj.name.Equals("FirePos"))
            {
                m_firePos = obj;
            }
        }




        m_type = m_weaponData.m_type;
        m_id = m_weaponData.m_id;
        m_name = m_weaponData.m_name;
        m_damage = m_weaponData.m_damage;
        m_createBulletCount = m_weaponData.m_createBulletCount;
        m_speed = m_weaponData.m_speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
