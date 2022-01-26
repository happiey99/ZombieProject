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
        player = GameObject.Find("Player");
        realCam = Camera.main.transform;
        transform.position = player.transform.position;
        transform.rotation = Quaternion.identity;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        transform.LookAt(player.transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        xSpeed = 20f;
        ySpeed = 20f;
        cameraTime = 0.3f;
        cameraMoveSpeed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
    }

    private void LateUpdate()
    {
        Follow();
        BlockCamera();
    }

    RaycastHit hit;
    public void BlockCamera()
    {
        Vector3 Distance = Vector3.zero;
        float camDistance = 0.0f;

        if (Physics.Linecast(player.transform.position, transform.position, out hit)) 
        {
            camDistance = (player.transform.position - hit.transform.position).magnitude;
            Distance = (player.transform.position - hit.transform.position).normalized;

            realCam.localPosition = Vector3.Lerp(realCam.transform.position, camDistance * Distance,0.5f);
        }
        else
        {

        }
   

    }
   

    public void LookAround()
    {
        yCam += Input.GetAxis("Mouse X") * Time.deltaTime * xSpeed;
        xCam += Input.GetAxis("Mouse Y") * Time.deltaTime * ySpeed;
        xCam = Mathf.Clamp(xCam, -30, 10);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(xCam, yCam, transform.rotation.z), cameraTime * Time.deltaTime * cameraMoveSpeed);
    }

    public void Follow()
    {
        moveToPlayer = Vector3.Lerp(transform.position, player.transform.position, cameraTime * Time.deltaTime * cameraMoveSpeed);
        transform.position = moveToPlayer;
    }
}
