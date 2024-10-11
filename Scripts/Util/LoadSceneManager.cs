using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
public class LoadSceneManager : DontDestroy<LoadSceneManager>
{
    public enum eSceneState
    {
        None = -1,
        Title,
        Menu,
        Game,
        Boss
    }   

    public AsyncOperation getSceneInfo() { return m_loadSceneInfo; }
    AsyncOperation m_loadSceneInfo;
    eSceneState m_state = eSceneState.Title;
    eSceneState m_loadState;


    public void gotoMenu()
    {
        LoadSceneAsyc(eSceneState.Menu);
    }
    public eSceneState GetState()
    {
        return m_state;
    }
    public void SetState(eSceneState state)
    {
        m_state = state;
    }
    public void LoadSceneAsyc(eSceneState scene)
    {
        if(m_loadState != eSceneState.None)
        {
            return;
        }
        if(m_state == eSceneState.Menu)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        m_loadState = scene;
        m_loadSceneInfo = SceneManager.LoadSceneAsync(scene.ToString());
        m_loadSceneInfo.allowSceneActivation = false;
    }
    public void LoadSceneMerge(eSceneState scene)
    {
        if (m_loadState != eSceneState.None)
        {
            return;
        }
        m_loadState = scene;
        m_loadSceneInfo = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);
    }
    protected override void OnAwake()
    {
        
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {        
        m_loadState = eSceneState.None;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_loadSceneInfo != null && m_loadState != eSceneState.None)
        {
            if(m_loadSceneInfo.isDone)
            {
                m_loadSceneInfo = null;
                m_state = m_loadState;
                m_loadState = eSceneState.None;
                Debug.Log(m_state.ToString() + "씬 로드 완료!");
            }
            else
            {
                Debug.Log(m_loadSceneInfo.progress);
            }
        }
        /*
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(PopupManager.Instance.CheckEscape())
                {
                    switch(m_state)
                    {
                        case eSceneState.Title:
                            PopupManager.Instance.OpenPopupOKCancel("[000000]Notice[-]", "[000000]게임을 종료하시겠습니까?", () =>
                            {
#if UNITY_EDITOR
                                EditorApplication.isPlaying = false;
#else
                                Application.Quit();                          
#endif

                            }, null, "예", "아니오");
                            break;
                        case eSceneState.Lobby:
                            PopupManager.Instance.OpenPopupOKCancel("[000000]Notice", "타이틀 화면으로 돌아가시겠습니까?", () =>
                            {
                                PopupManager.Instance.ClosePopup();
                                LoadSceneAsyc(eSceneState.Title);
                            }, null, "예", "아니오");
                            break;
                        case eSceneState.Game:
                            PopupManager.Instance.OpenPopupOKCancel("[000000]Notice", "로비로 돌아가시겠습니까?", () =>
                            {
                                PopupManager.Instance.ClosePopup();
                                LoadSceneAsyc(eSceneState.Lobby);
                            }, null, "예", "아니오");
                            break;
                    }
                }
            }
        }
        */
    }
}
