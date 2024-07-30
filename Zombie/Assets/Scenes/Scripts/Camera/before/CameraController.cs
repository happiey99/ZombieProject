using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform cameraTarget;

    float x = 0;
    float y = 0;

    Vector3 cameraOriPosition;

    GameObject player;

    Transform lookHere;

    public int masks;


    float layDist;

    void Start()
    {
        player = GameObject.Find("Player");
        cameraTarget = player.transform.GetChild(0);

        Cursor.lockState = CursorLockMode.Locked;
        cameraOriPosition = new Vector3(0f, 0, -2);
        lookHere = transform.GetChild(1);

        masks = (1 << 0) | (1 << 3);
    }



    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, cameraTarget.position,1 * Time.deltaTime*30);
        Camera.main.transform.LookAt(lookHere);

        LookAround();
        MouseWheel();

    }

    //private void LateUpdate()
    //{
    //    //CameraRayHitWall();

    //}

    void LookAround()
    {
        x += Input.GetAxis("Mouse X");
        y += Input.GetAxis("Mouse Y") * -1;

        y = Mathf.Clamp(y, -50, 50);

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x + y, transform.rotation.y + x, 0));
    }

    //Ray ray;
    //void CameraRayHitWall()
    //{
    //    Vector3 playerToCamera = (Camera.main.transform.position - cameraTarget.position);

    //    ray = new Ray(cameraTarget.position, playerToCamera.normalized);

    //    RaycastHit hit;

    //    Physics.Raycast(ray, out hit, playerToCamera.magnitude, masks) ;

    //    if (hit.point != Vector3.zero) 
    //        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, hit.point, 0.7f * Time.deltaTime * 10);          
    //    else
    //        Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, cameraOriPosition, 0.7f * Time.deltaTime * 10);
        

    //}
  
   
    float wheelVelue = -2;

    void MouseWheel()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        wheelVelue = Mathf.Clamp(wheelVelue, -3, -1);

        wheelVelue += scroll;

        cameraOriPosition = new Vector3(0f, 0, wheelVelue);
    }

}
