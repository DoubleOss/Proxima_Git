using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquieManager : DontDestroy<EquieManager>
{

    [SerializeField]
    PlayerControll m_player;

    public Dictionary<int, Weapon> m_dictKeyNumberToWeapon = new Dictionary<int, Weapon>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
