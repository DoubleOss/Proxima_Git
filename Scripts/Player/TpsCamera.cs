using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsCamera : MonoBehaviour
{

    [SerializeField]
    Transform objectToFollow;
    [SerializeField]
    float followSpeed = 10f;

    [SerializeField]
    float mouseSensitivity = 100f; // 마우스감도

    [SerializeField]
    float clampAngle = 70f; // 각도 최대값 

    [SerializeField]
    Transform mainCamera;
    [SerializeField]
    public Vector3 dirNormalize;
    [SerializeField]
    public Vector3 finalDir;

    [SerializeField]
    float minDistance = 0f;
    [SerializeField]
    float maxDistance = 10f;
    [SerializeField]
    float finalDistance = 0f;
    

    float rotationX;
    float rotationY;


    // Start is called before the first frame update
    void Start()
    {
        rotationX = transform.localRotation.eulerAngles.x;
        rotationY = transform.localRotation.eulerAngles.y;

        dirNormalize = mainCamera.localPosition.normalized;
        finalDistance = mainCamera.localPosition.magnitude;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

    }

    // Update is called once per frame
    void Update()
    {
        rotationX += -1* Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        //Clamp(값, 최소, 최대);
        rotationX = Mathf.Clamp(rotationX, -clampAngle, clampAngle);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);

    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position, followSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormalize * maxDistance);

        //벽 오브젝트 서치
        int layerMask = 1 << LayerMask.NameToLayer("Wall");
        RaycastHit hit;
        if(Physics.Linecast(transform.position, finalDir, out hit, layerMask))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }

        mainCamera.localPosition = Vector3.Lerp(mainCamera.localPosition, dirNormalize * finalDistance, Time.deltaTime * 10f);

    }
}
