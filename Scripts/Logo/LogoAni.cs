using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAni : MonoBehaviour
{
    [SerializeField]
    GameObject m_logo3;
    [SerializeField]
    GameObject m_logos;


    [SerializeField]
    GameObject m_schoolLogo3;



    public void LogoStartText()
    {
        m_logo3.SetActive(true);
    }

    public void LogoStart()
    {
        m_schoolLogo3.SetActive(false);
        m_logos.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_logos.SetActive(false);
        m_logo3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
