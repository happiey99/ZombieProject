using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform realCam;
    GameObject player;

    private float cameraMoveSpeed;
    private float xCam;
    private float yCam;

    private void Awake()
    {
        player = GameObject.Find("Player/LookHere");
        
        realCam = Camera.main.transform;
        transform.position = player.transform.position;
        transform.rotation = Quaternion.identity;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraMoveSpeed = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        realCam.LookAt(player.transform);
    }

    private void LateUpdate()
    {
        Follow();
        BlockCamera();
    }

    public void LookAround()
    {
        xCam += Input.GetAxis("Mouse Y") ;
        yCam += Input.GetAxis("Mouse X")  * -1;

        xCam = Mathf.Clamp(xCam, -60, 30);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(xCam, yCam, 0),0.1f);
    }

    public void Follow()
    {
        transform.position = player.transform.position;
    }
    
    RaycastHit hit;
    public void BlockCamera()
    {

    }

}
