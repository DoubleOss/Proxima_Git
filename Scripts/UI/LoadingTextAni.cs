using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingTextAni : MonoBehaviour
{
    // Start is called before the first frame update

    TextMeshProUGUI m_text;

    float m_time = 0f;

    float m_maxTime = 1.32f;

    void Start()
    {
        m_text = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime;

        if(m_time > m_maxTime)
        {
            m_time = 0f;
            m_text.text += ".";

            if (m_text.text.Equals("리소스를 불러오고 있습니다...."))
            {
                m_text.text = "리소스를 불러오고 있습니다.";
            }
        }
    }
}
