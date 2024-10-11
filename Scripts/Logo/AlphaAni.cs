using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlphaAni : MonoBehaviour
{

    [SerializeField]
    int m_maxTime = 4;

    float m_time = 0;

    Image m_iamge;

    bool m_sceneLoad = false;

    [SerializeField]
    TextMeshProUGUI loadingMessage;

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
        if (m_time > m_maxTime && ! m_sceneLoad)
        {
            m_sceneLoad = true;
            loadingMessage.gameObject.SetActive(true);
            LoadSceneManager.Instance.LoadSceneAsyc(LoadSceneManager.eSceneState.Menu);
            float per = Mathf.Round(LoadSceneManager.Instance.getSceneInfo().progress * 100); 
            loadingMessage.text = "게임을 불러오고 있습니다 " + per + " %";
        }
        else
        {
            m_time += Time.deltaTime;
            float currentAlpha = (1f / m_maxTime) * m_time;
            m_iamge.color = new Color(m_iamge.color.r, m_iamge.color.g, m_iamge.color.b, currentAlpha);
        }
        if(m_sceneLoad)
        {
            float per = Mathf.Round(LoadSceneManager.Instance.getSceneInfo().progress * 100);
            loadingMessage.text = "게임을 불러오고 있습니다 " + per + " %";
            if(LoadSceneManager.Instance.getSceneInfo().progress >= 90f)
            {
                loadingMessage.text = "게임을 불러오고 있습니다 " + 100 + " %";
            }
        }



    }
}
