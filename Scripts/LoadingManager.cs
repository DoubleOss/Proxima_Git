using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingManager : SingletonMonoBehaviour<LoadingManager> 
{
    [SerializeField]
    GameObject loadingObj;

    [SerializeField]
    TextMeshProUGUI text;
    // Start is called before the first frame update

    float m_tick = 0;
    float m_maxTime = 3f;

    public void SetActiveLoading()
    {
        loadingObj.SetActive(true);
    }

    protected override void OnStart()
    {
        loadingObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (LoadSceneManager.Instance.getSceneInfo() != null)
        {
            Debug.Log(LoadSceneManager.Instance.getSceneInfo().isDone);
            if (LoadSceneManager.Instance.getSceneInfo().progress >= 0.9f)
            {
                m_tick += Time.deltaTime;
                if (m_tick > m_maxTime)
                {
                    LoadSceneManager.Instance.getSceneInfo().allowSceneActivation = true;
                }
            }
            float per = LoadSceneManager.Instance.getSceneInfo().progress;
            text.text = "데이터를 불러오고 있습니다. " + Mathf.Floor(per * 100f) + " %";

        }
    }
}
