using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField]
    BossMonster monster;


    float m_current = 0;

    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if( monster.m_health > 0)
        {
            m_current += Time.deltaTime;
            int min = (int)(m_current / 60);
            int sec = (int)(m_current % 60);

            text.text = string.Format("{0:00}:{1:00}",min, sec);
        }


        

    }
}
