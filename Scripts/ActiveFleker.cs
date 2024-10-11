using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFleker : MonoBehaviour
{

    float m_tick = 0;
    float m_maxTick = 0.5f;

    Material m_material;

    Color m_orign;
    bool m_view = true; //false면 안보임

    // Start is called before the first frame update
    void Start()
    {
        m_material = GetComponent<MeshRenderer>().material;
        m_orign = m_material.color;
    }

    // Update is called once per frame
    void Update()
    {
        m_tick += Time.deltaTime;
        if (m_tick >= m_maxTick)
        {
            m_tick = 0;
            if(m_view)
            {
                m_view = false;
                m_material.color = new Color(m_orign.r, m_orign.g, m_orign.b, 0f);
            }
            else
            {
                m_view = true;
                m_material.color = new Color(m_orign.r, m_orign.g, m_orign.b, 1f);
            }

        }
    }
}
