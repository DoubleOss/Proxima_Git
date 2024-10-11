using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponLoreAutoCreate : MonoBehaviour
{
    [SerializeField]
    GameObject m_weaponSlotLore;

    [SerializeField]
    Transform m_slotStartPos;

    // Start is called before the first frame update
    void Start()
    {
        m_weaponSlotLore = Resources.Load("Prefab/UI/EquieSlot") as GameObject;
        WeaponManager manager = WeaponManager.Instance;
        Debug.Log(m_slotStartPos.transform.localPosition);
        for(int i = 0; i < manager.maxWeaponDataCount; i++)
        {
            float xPos = m_slotStartPos.transform.localPosition.x + i % 4 * 164f;
            float yPos = m_slotStartPos.transform.localPosition.y + i / 4 * -190f;

            var obj = Instantiate(m_weaponSlotLore);
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = new Vector3(xPos, yPos, 0);

            WeaponManager.WeaponData data = manager.GetWeaponData(i).Value;
            obj.GetComponent<WeaponLore>().weaponData = data;
            Debug.Log("Image/WeaponIcon/" + data.m_spriteType.ToString());
            obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/WeaponIcon/" + data.m_spriteType.ToString());


        }

        float x = m_slotStartPos.transform.localPosition.x + manager.maxWeaponDataCount % 4 * 164f;
        float y = m_slotStartPos.transform.localPosition.y + manager.maxWeaponDataCount / 4 * -190f;

        var obj2 = Instantiate(m_weaponSlotLore);
        obj2.transform.SetParent(gameObject.transform);
        obj2.transform.localPosition = new Vector3(x, y, 0);

        WeaponManager.WeaponData data2 = manager.GetWeaponData(99).Value;
        obj2.GetComponent<WeaponLore>().weaponData = data2;
        Debug.Log("Image/WeaponIcon/" + data2.m_spriteType.ToString());
        obj2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/WeaponIcon/" + data2.m_spriteType.ToString());


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
