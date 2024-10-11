using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsMove : MonoBehaviour
{

    [SerializeField] 
    GameObject cameraHolder;
    [SerializeField]
    float mouseSensitivity;
    public CharacterController m_controller;

    public Transform m_cam;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public float m_speed = 6f;

    float verticalLookRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -40f, 80f); //카메라 회전 각도 (최소: -40, 최대: +80)

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;


        m_controller.Move(direction * m_speed * Time.deltaTime);

    }
}
