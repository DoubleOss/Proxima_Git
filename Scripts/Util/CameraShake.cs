using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    float m_duration = 1f;
    [SerializeField]
    float m_power = 0.5f;
    float m_checkTick = 0f;
    bool m_isStart;
    [SerializeField]
    Camera m_main;

    Vector3 m_orginPos;

    TpsCamera m_tpsCamera;

    public void ShakeCamera(float duration, float power)
    {
        m_checkTick = 0f;
        m_isStart = true;
        m_duration = duration;
        m_power = power;
        m_orginPos = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_orginPos = transform.position;
        m_tpsCamera = GetComponent<TpsCamera>();
    }
    // Update is called once per frame
    void Update()
    {
        if (m_isStart)
        {
            m_checkTick += Time.deltaTime;
            var dir = Random.insideUnitSphere.normalized;
            if (m_checkTick > m_duration)
            {
                //transform.position = m_orginPos;
                m_checkTick = 0f;
                m_isStart = false;
            }
        }
    }
}
