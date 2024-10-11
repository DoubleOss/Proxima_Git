using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
