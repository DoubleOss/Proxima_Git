using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : SingletonMonoBehaviour<WeaponManager> 
{

    public enum eWeaponType
    {
        RIFLE,
        PISTOL,
        SWORLD,
        SHOTGUN,
        HAND,
        SHIELD,
        SHELL,
        SHOOTER,
        GRESS_HOPER,
        SENKOO,
        NONE
    }
    public enum eShootType
    {
        HOUND,
        ASTEROID,
        METEORA,
        NONE
    }
    public enum eSpriteType
    {
        Bullet,
        Option,
        Pistol,
        Shield,
        Sniper,
        Sword,
        NONE
    }
    public struct WeaponData
    {
        public eWeaponType m_type;
        public eShootType m_shooterType;
        public eSpriteType m_spriteType;
        public int m_id;
        public string m_name;
        public float m_damage;
        public int m_createBulletCount;
        public float m_speed;
        public float requestHealth;
        public float m_delay;
    }

    public int maxWeaponDataCount = 0;
    Dictionary<int, WeaponData> m_weaponDicTable = new Dictionary<int, WeaponData>();
    
    public WeaponData? GetWeaponData(int id)
    {
        if (!m_weaponDicTable.ContainsKey(id)) return null;
        return m_weaponDicTable[id];
    }

    void InitWeaponData()
    {
        int weaponId = 0;
        m_weaponDicTable.Add(weaponId, new WeaponData()
        {
            m_type = eWeaponType.RIFLE,
            m_shooterType = eShootType.ASTEROID,
            m_spriteType = eSpriteType.Pistol,
            m_id = weaponId++,
            m_name = "0번 라이플",
            m_damage = 8,
            m_createBulletCount = 1,
            m_speed = 20f,
            requestHealth = 0.8f,
            m_delay = 0.15f,
        });
        m_weaponDicTable.Add(weaponId, new WeaponData()
        {
            m_type = eWeaponType.SWORLD,
            m_shooterType = eShootType.NONE,
            m_spriteType = eSpriteType.Sword,
            m_id = weaponId++,
            m_name = "1번 카타나",
            m_damage = 4,
            m_createBulletCount = 0,
            m_speed = 0,
            requestHealth = 1f,
            m_delay = 0.01f,
        });
        m_weaponDicTable.Add(weaponId, new WeaponData()
        {
            m_type = eWeaponType.PISTOL,
            m_shooterType = eShootType.ASTEROID,
            m_spriteType = eSpriteType.Pistol,
            m_id = weaponId++,
            m_name = "2번 권총",
            m_damage = 12,
            m_createBulletCount = 1,
            m_speed = 14,
            requestHealth = 0.8f,
            m_delay = 0.32f,
        });
        m_weaponDicTable.Add(weaponId, new WeaponData()
        {
            m_type = eWeaponType.SHOOTER,
            m_shooterType = eShootType.ASTEROID,
            m_spriteType = eSpriteType.Bullet,
            m_id = weaponId++,
            m_name = "3번 슈터",
            m_damage = 20,
            m_createBulletCount = 1,
            m_speed = 18,
            requestHealth = 7f,
            m_delay = 0.32f,
        });
        m_weaponDicTable.Add(weaponId, new WeaponData()
        {
            m_type = eWeaponType.SHOOTER,
            m_shooterType = eShootType.HOUND,
            m_spriteType = eSpriteType.Bullet,
            m_id = weaponId++,
            m_name = "4번 슈터",
            m_damage = 7,
            m_createBulletCount = 1,
            m_speed = 20,
            requestHealth = 7f,
            m_delay = 0.32f,
        });
        m_weaponDicTable.Add(weaponId, new WeaponData()
        {
            m_type = eWeaponType.SHOOTER,
            m_shooterType = eShootType.METEORA,
            m_spriteType = eSpriteType.Bullet,
            m_id = weaponId++,
            m_name = "5번 슈터",
            m_damage = 10,
            m_createBulletCount = 1,
            m_speed = 12,
            requestHealth = 10f,
            m_delay = 0.32f,
        });
        m_weaponDicTable.Add(weaponId, new WeaponData()
        {
            m_type = eWeaponType.SHIELD,
            m_shooterType = eShootType.NONE,
            m_spriteType = eSpriteType.Shield,
            m_id = weaponId++,
            m_name = "6번 실드",
            m_damage = 0,
            m_createBulletCount = 0,
            m_speed = 0,
            requestHealth = 100f,
            m_delay = 6f,
        });
        m_weaponDicTable.Add(weaponId, new WeaponData()
        {
            m_type = eWeaponType.GRESS_HOPER,
            m_shooterType = eShootType.NONE,
            m_spriteType = eSpriteType.Option,
            m_id = weaponId++,
            m_name = "7번 호퍼",
            m_damage = 0,
            m_createBulletCount = 0,
            m_speed = 0,
            requestHealth = 3f,
            m_delay = 0.7f,
        });
        m_weaponDicTable.Add(weaponId, new WeaponData()
        {
            m_type = eWeaponType.SENKOO,
            m_shooterType = eShootType.NONE,
            m_spriteType = eSpriteType.Option,
            m_id = weaponId++,
            m_name = "8번 선공",
            m_damage = 10,
            m_createBulletCount = 0,
            m_speed = 0,
            requestHealth = 8f,
            m_delay = 10f,
        });

        m_weaponDicTable.Add(99, new WeaponData()
        {
            m_type = eWeaponType.NONE,
            m_shooterType = eShootType.NONE,
            m_spriteType = eSpriteType.NONE,
            m_id = 99,
            m_name = "빈 슬롯",
            m_damage = 0,
            m_createBulletCount = 0,
            m_speed = 0,
            requestHealth = 0,
            m_delay = 0,
        });

        maxWeaponDataCount = weaponId;
    }
    protected override void OnAwake()
    {
        InitWeaponData();
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
