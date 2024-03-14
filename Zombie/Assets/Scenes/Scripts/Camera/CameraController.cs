using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Transform cameraTarget;

    float x = 0;
    float y = 0;

    Vector3 cameraOriPosition;

    GameObject player;

    Transform lookHere;

    public int masks;


    void Start()
    {
        player = GameObject.Find("Player");
        cameraTarget = player.transform.GetChild(0);

        Cursor.lockState = CursorLockMode.Locked;
        cameraOriPosition = new Vector3(0.5f, 0, -2);
        lookHere = transform.GetChild(1);

        masks = (1 << 0) | (1 << 3);
    }



    void Update()
    {
        LookAround();
        MouseWheel();
        CameraRayHitWall();
    }
    
    // private void LateUpdate()
   

    private void LateUpdate()
    {
        transform.position = cameraTarget.position;
        Camera.main.transform.LookAt(lookHere);

        
      
    }


    void LookAround()
    {
        x += Input.GetAxis("Mouse X");
        y += Input.GetAxis("Mouse Y") * -1;

        y = Mathf.Clamp(y, -50, 50);

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x + y, transform.rotation.y + x, 0));
    }


    public Vector3 vec; 
    void CameraRayHitWall()
    {
        
        Vector3 CameraRayDir = (Camera.main.transform.position - cameraTarget.position);
        float rayDis = CameraRayDir.magnitude;
        Ray ray = new Ray(cameraTarget.position, CameraRayDir);
        RaycastHit hit;

        

        if (Physics.Raycast(ray, out hit, rayDis, masks))
        {
            vec = hit.transform.position;
            Camera.main.transform.position =Vector3.Lerp(Camera.main.transform.position, hit.point, 0.7f * Time.deltaTime * 10);
        }
        else
        {
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition,cameraOriPosition,0.7f * Time.deltaTime * 10);
        }

    }

    float wheelVelue = -2;

    void MouseWheel()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        wheelVelue = Mathf.Clamp(wheelVelue, -10, -1);

        wheelVelue += scroll;

        cameraOriPosition = new Vector3(0.5f, 0, wheelVelue);
    }

}
