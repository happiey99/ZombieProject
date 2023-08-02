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


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = transform.parent.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Camera.main.transform.LookAt(cameraTarget);
        LookAround();
        AimPositioin();
        ClampAroundHead();
        CameraDistance();
        //CameraRayHitWall();
    }

  
    void LookAround()
    {
        x += Input.GetAxis("Mouse X");
        y += Input.GetAxis("Mouse Y") * -1;

        y = Mathf.Clamp(y, -50, 50);

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x + y, transform.rotation.y + x, 0));
    }

    public float distance;

    void CameraDistance()
    {

    }

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
        float dis = CameraRayDir.magnitude;

        Ray ray = new Ray(cameraTarget.position, CameraRayDir);

        if(Physics.Raycast(ray, dis))
        {
            
        }
        else
        {

        }
        
    }

}
