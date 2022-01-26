using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    Vector3 moveToPlayer;
    Transform realCam;
    float yCam;
    float xCam;

    GameObject player;
    float xSpeed;
    float ySpeed;
    float cameraTime;
    float cameraMoveSpeed;


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
        xSpeed = 20f;
        ySpeed = 20f;
        cameraTime = 0.3f;
        cameraMoveSpeed = 40f;
        
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

    RaycastHit hit;

    public void BlockCamera()
    {
    
      
   
    }


    public void LookAround()
    {
        yCam += Input.GetAxis("Mouse X") * Time.deltaTime * xSpeed;
        xCam += -Input.GetAxis("Mouse Y") * Time.deltaTime * ySpeed;
        xCam = Mathf.Clamp(xCam, -30, 10);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(xCam, yCam, transform.rotation.z), cameraTime * Time.deltaTime * cameraMoveSpeed);
    }

    public void Follow()
    {
        moveToPlayer = Vector3.Lerp(transform.position, player.transform.position, cameraTime * Time.deltaTime * cameraMoveSpeed);
        transform.position = moveToPlayer;
    }
}
