using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CameraController : MonoBehaviour
{
    public Rig rig;

    public Transform aimTarget;
    public Transform cameraTarget;

    float x = 0;
    float y = 0;


    PlayerController player;

    public Vector3 cameraOriPosition;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = transform.parent.GetComponent<PlayerController>();


        cameraOriPosition = new Vector3(0, 0, -2);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Camera.main.transform.LookAt(cameraTarget);
        LookAround();
        MouseWheel();

        AimPositioin();
        ClampAroundHead();

        CameraRayHitWall();
    }


    void LookAround()
    {
        x += Input.GetAxis("Mouse X");
        y += Input.GetAxis("Mouse Y") * -1;

        y = Mathf.Clamp(y, -50, 50);

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x + y, transform.rotation.y + x, 0));
    }

    public float distance;


    void ClampAroundHead()
    {
        if (transform.localEulerAngles.y <= 100 &&
            transform.localEulerAngles.y >= -10 ||
            transform.localEulerAngles.y <= 370 &&
            transform.localEulerAngles.y >= 260)
        {
            rig.weight = Mathf.Lerp(rig.weight, 1, 0.1f);
        }
        else
        {
            rig.weight = Mathf.Lerp(rig.weight, 0, 0.1f);
        }

    }


    void AimPositioin()
    {
        Vector2 midOfS = new Vector2(Screen.width / 2f, Screen.height / 2f);

        Ray ray = Camera.main.ScreenPointToRay(midOfS);

        RaycastHit[] hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.layer != 6)
            {
                aimTarget.position = hits[i].point;
                break;
            }
        }
    }


    void CameraRayHitWall()
    {
        Vector3 CameraRayDir = (Camera.main.transform.position - cameraTarget.position).normalized;
        float rayDis = CameraRayDir.magnitude;
        Ray ray = new Ray(cameraTarget.position, CameraRayDir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDis))
        {
            Camera.main.transform.position =
                Vector3.Lerp(Camera.main.transform.position,
                hit.point,
                0.7f * Time.deltaTime * 10);
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
