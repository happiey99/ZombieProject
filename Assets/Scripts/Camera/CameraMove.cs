using Unity.Mathematics;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject LookHere;
    private float xCam;
    private float yCam;
    PlayerController pc;

    Vector3 oriPos;
    [SerializeField] Transform LookAt;
    [SerializeField] Transform zoomPoint;

    private void Awake()
    {
        pc = LookHere.transform.parent.GetComponent<PlayerController>();

        Camera.main.transform.localPosition = new Vector3(0.5f, 1.5f, -4f);
        oriPos = new Vector3(0.5f, 1.5f, -4f);
        transform.position = LookHere.transform.position;
        transform.rotation = Quaternion.identity;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Managers._charState.invenOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            LookAround();
        }
       
        Camera.main.transform.LookAt(LookAt);//lookhere

    }

    private void LateUpdate()
    {
        Follow();
        BlockCamera();
        Rebound();
    }

    public void LookAround()
    {
        xCam += Input.GetAxis("Mouse Y") * -1;
        yCam += Input.GetAxis("Mouse X");
        xCam = Mathf.Clamp(xCam, -60, 30);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(xCam, yCam, 0), 0.1f);
    }
    void Rebound()
    {
        if (!Managers._charState.isAttack||Managers._charState.isEmpty)
            return;
        yCam += UnityEngine.Random.Range(-0.2f, 0.2f);
        xCam -= 0.1f;
    }

    public void Follow()
    {
        transform.position = LookHere.transform.position;//lookhere
    }

    RaycastHit hit;

    //[SerializeField] float maxDistans;
    //[SerializeField] float minDistans;
    //[SerializeField] float zoomDistans;
    //float finalDist;
    public void BlockCamera()
    {
        bool isHit = Physics.Linecast(LookAt.transform.position, Camera.main.transform.position, out hit, LayerMask.GetMask("Default"));
        //Camera.main.transform.localRotation = Quaternion.Euler(new Vector3(20, 0, 0));


        if (isHit)
        {
            //finalDist = hit.distance;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, hit.point, 5 * Time.deltaTime);
        }
        else
        {
            if (!Managers._charState.isAim)
            {
                // finalDist = maxDistans;
                Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, oriPos, 5 * Time.deltaTime);
            }
            else
            {
                // finalDist = zoomDistans;
                Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, (oriPos - LookAt.transform.localPosition).normalized * 2, 5 * Time.deltaTime);
                // Camera.main.transform.localRotation = Quaternion.Euler(new Vector3(20, 0, 0));
            }
        }

        //  Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, Camera.main.transform.localPosition.normalized * finalDist, 5 * Time.deltaTime);
    }

}