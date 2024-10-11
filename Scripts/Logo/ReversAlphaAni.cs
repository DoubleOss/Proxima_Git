using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReversAlphaAni : MonoBehaviour
{

    [SerializeField]
    int m_maxTime = 2;

    float m_time = 0;

    Image m_iamge;

    // Start is called before the first frame update
    void Start()
    {
        m_time = 0;
        m_iamge = GetComponent<Image>();
        m_iamge.color = new Color(m_iamge.color.r, m_iamge.color.g, m_iamge.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime;
        float currentAlpha = 1f-(1f / m_maxTime) * m_time;
        m_iamge.color = new Color(m_iamge.color.r, m_iamge.color.g, m_iamge.color.b, currentAlpha);

        if(currentAlpha <= 0f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
