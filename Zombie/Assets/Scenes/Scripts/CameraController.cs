using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   
    public Transform cameraTarget;

    float x = 0;
    float y = 0;

    Vector3 cameraOriPosition;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraOriPosition = new Vector3(0, 0, -2);
    }

 
    void LateUpdate()
    {
        Camera.main.transform.LookAt(cameraTarget);
        LookAround();
        MouseWheel();
        CameraRayHitWall();
    }


    void LookAround()
    {
        x += Input.GetAxis("Mouse X");
        y += Input.GetAxis("Mouse Y") * -1;

        y = Mathf.Clamp(y, -50, 50);

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x + y, transform.rotation.y + x, 0));
    }



    void CameraRayHitWall()
    {
        Vector3 CameraRayDir = (Camera.main.transform.position - cameraTarget.position);
        float rayDis = CameraRayDir.magnitude;
        Ray ray = new Ray(cameraTarget.position, CameraRayDir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDis))
        {
            Camera.main.transform.position =
                Vector3.Lerp(Camera.main.transform.position,
                hit.point,
                0.7f * Time.deltaTime * 10);
            Debug.Log(hit.transform.gameObject.name);
        }
        else
        {
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition,
               cameraOriPosition,
                0.7f * Time.deltaTime * 10);
        }

    }

    float wheelVelue = -2;

    void MouseWheel()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
      
        wheelVelue = Mathf.Clamp(wheelVelue, -10, -1);

        wheelVelue += scroll;

        cameraOriPosition = new Vector3(0, 0, wheelVelue);
    }

}
