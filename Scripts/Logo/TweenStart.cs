using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenStart : MonoBehaviour
{

    TweenTransform tweenTransform;

    public void tweenPlay()
    {
        tweenTransform.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        tweenTransform = GetComponent<TweenTransform>();

        tweenTransform.ResetToBeginning();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
