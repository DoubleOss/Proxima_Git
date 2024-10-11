using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponSlotAuto : MonoBehaviour
{

    [SerializeField]
    GameObject[] mainObjs;
    [SerializeField]
    GameObject[] subObjs;




    // Start is called before the first frame update
    void Start()
    {
        int i = 0;

        PlayerSelectWeaponManager manager = PlayerSelectWeaponManager.Instance;
        foreach (GameObject objs in mainObjs)
        {
            WeaponManager.WeaponData data = manager.m_mainWeapon[i];
            objs.GetComponentInChildren<Transform>().GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/WeaponIcon/" + manager.m_mainWeapon[i].m_spriteType.ToString());
            i++;
        }
        i = 0;
        foreach (GameObject objs in subObjs)
        {
            WeaponManager.WeaponData data = manager.m_subWeapon[i];
            objs.GetComponentInChildren<Transform>().GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/WeaponIcon/" + manager.m_subWeapon[i].m_spriteType.ToString());
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
