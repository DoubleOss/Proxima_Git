using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    GameObject m_bgObject;
    [SerializeField]
    GameObject m_titleObject;

    float m_tick = 0;
    float m_maxTime = 3f;
    public void SetTitle()
    {
        LoadSceneManager.Instance.SetState(LoadSceneManager.eSceneState.Menu);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey && !Input.GetKeyDown(KeyCode.Escape) && LoadSceneManager.Instance.GetState() != LoadSceneManager.eSceneState.Title)
        {
            LoadSceneManager.Instance.LoadSceneAsyc(LoadSceneManager.eSceneState.Menu);
        }

        if(LoadSceneManager.Instance.getSceneInfo() != null)
        {
            Debug.Log(LoadSceneManager.Instance.getSceneInfo().isDone);
            if(LoadSceneManager.Instance.getSceneInfo().progress >= 0.9f)
            {
                m_tick += Time.deltaTime;
                if (m_tick > m_maxTime)
                {
                    LoadSceneManager.Instance.getSceneInfo().allowSceneActivation = true;
                }
            }

        }
    }
}
