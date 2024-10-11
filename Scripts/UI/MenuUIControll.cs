using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MenuUIContorll : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject m_selectSingleObj;
    [SerializeField]
    GameObject m_selectMultiObj;
    [SerializeField]
    GameObject m_practiceMultiObj;
    [SerializeField]
    GameObject m_rightMenuObj;
    [SerializeField]
    GameObject[] unSelectObjs = new GameObject[3];

    [SerializeField]
    GraphicRaycaster m_rayCaster;

    PointerEventData m_rayHit;



    [SerializeField]
    GameObject m_uiObj;

    [SerializeField]
    GameObject m_lobbyMenu;
    [SerializeField]
    GameObject m_equieMenu;

    float loadTime = 0f;
    float loadMaxTime = 4f;

    bool m_loadUi = false;
    bool m_lobbyToEquie = false;
    bool m_equieToLobby = false;


    public void GoSinglePlayMod()
    {
        LoadingManager.Instance.SetActiveLoading();
        LoadSceneManager.Instance.LoadSceneAsyc(LoadSceneManager.eSceneState.Boss);
    }
    public void GoPractiveGame()
    {
        LoadingManager.Instance.SetActiveLoading();
        LoadSceneManager.Instance.LoadSceneAsyc(LoadSceneManager.eSceneState.Game);
    }
    public void moveToEquieUi()
    {
        m_uiObj.SetActive(true);
        m_loadUi = true;
        m_lobbyToEquie = true;

    }

    public void moveToLobbyUi()
    {
        m_uiObj.SetActive(true);
        m_loadUi = true;
        m_equieToLobby = true;

    }

    void Start()
    {
        m_selectSingleObj.SetActive(false);
        m_selectMultiObj.SetActive(false);
        m_practiceMultiObj.SetActive(false);

        m_rayHit = new PointerEventData(null);
        m_uiObj.SetActive(false);
        m_equieMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(m_loadUi)
        {
            loadTime += Time.deltaTime;
            if(loadTime >= loadMaxTime)
            {
                m_loadUi = false;
                m_uiObj.SetActive(false);
                loadTime = 0;

                m_lobbyMenu.SetActive(false);
                m_equieMenu.SetActive(false);


                if (m_equieToLobby)
                {
                    m_lobbyMenu.SetActive(true);

                }
                else if (m_lobbyToEquie)
                {
                    m_equieMenu.SetActive(true);
                }
                m_equieToLobby = false;
                m_lobbyToEquie = false;


            }
        }

        GameObject obj;
        m_rayHit.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        m_rayCaster.Raycast(m_rayHit, results);
        // results 검출시 로직
        if (results.Count > 0)
        {
            //UI 태그 검색
            if(results[0].gameObject.CompareTag("SelectUi"))
            {
                obj = results[0].gameObject;

                GameObject selectObj = null;
                if (obj.name.Equals("Btn_SinglePlay"))
                {
                    
                    m_selectSingleObj.SetActive(true);
                    selectObj = m_selectSingleObj;
                    obj.SetActive(false);
                }
                if (obj.name.Equals("Btn_MultiPlay"))
                {
                    m_selectMultiObj.SetActive(true);
                    selectObj = m_selectMultiObj;
                    obj.SetActive(false);
                }
                if (obj.name.Equals("Btn_Practice"))
                {
                    m_practiceMultiObj.SetActive(true);
                    selectObj = m_practiceMultiObj;
                    obj.SetActive(false);
                }

                if ( selectObj != null)
                {
                    TweenScale tween = selectObj.GetComponent<TweenScale>();
                    tween.ResetToBeginning();
                    tween.PlayForward();
                }

            }

        }
        else if (results.Count == 0)
        {
            if(m_selectSingleObj.activeSelf) 
            {
                m_selectSingleObj.SetActive(false);
            }
            if(m_selectMultiObj.activeSelf)
            {
                m_selectMultiObj.SetActive(false);
            }
            if(m_practiceMultiObj.activeSelf)
            {
                m_practiceMultiObj.SetActive(false);
            }

            foreach (GameObject objs in unSelectObjs)
            {
                objs.SetActive(true);
            }
        }

    }
}
