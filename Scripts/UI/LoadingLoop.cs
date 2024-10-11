using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingLoop : MonoBehaviour
{


    float m_time = 0f;
    float m_maxTime = 1.25f;

    bool m_loopEed = false;

    TweenRotation m_tweenRotation;
    public void endAni()
    {
        m_loopEed = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_tweenRotation = GetComponent<TweenRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_loopEed)
        {
            m_time += Time.deltaTime;
            if(m_time >= m_maxTime)
            {
                m_maxTime = 0;
                m_loopEed = false;
                m_tweenRotation.ResetToBeginning();
                m_tweenRotation.PlayForward();

            }
        }
    }
}
