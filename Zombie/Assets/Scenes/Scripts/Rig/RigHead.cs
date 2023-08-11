using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigHead : MonoBehaviour
{
    Rig rig;


    Transform cameraAim;
    Transform aimTarget;

    [SerializeField]
    MultiAimConstraint head;

    [SerializeField]
    MultiAimConstraint body;

    [SerializeField]
    MultiAimConstraint aim;

    [SerializeField]
    TwoBoneIKConstraint secondhand;

    PlayerAnimation ani;

    private void Start()
    {
        rig = GetComponent<Rig>();
        ani = transform.parent.GetComponent<PlayerAnimation>();

        cameraAim = Camera.main.transform.parent;
        aimTarget = cameraAim.transform.GetChild(0);


        Init();
    }

    void Init()
    {
        body.weight = 0;
        aim.weight = 0;
        secondhand.weight = 0;
    }
    
    private void LateUpdate()
    {
        ClampAroundHead();
        AimRig();
    }

    void ClampAroundHead()
    {
        if(cameraAim.transform.eulerAngles.y <=80||
            cameraAim.transform.eulerAngles.y >280)
        {
            head.weight = Mathf.Lerp(head.weight, 1, Time.deltaTime * 7);
        }
        else
        {
            head.weight = Mathf.Lerp(head.weight, 0, Time.deltaTime * 7);
        }

    }

    void AimRig()
    {
        if (ani._isAim)
        {
            body.weight = Mathf.Lerp(body.weight, 1, Time.deltaTime * 7);
            aim.weight = Mathf.Lerp(aim.weight, 1, Time.deltaTime * 7);
            secondhand.weight = Mathf.Lerp(secondhand.weight, 1, Time.deltaTime * 7);
        }
        else
        {
            body.weight = Mathf.Lerp(body.weight, 0, Time.deltaTime * 7);
            aim.weight = Mathf.Lerp(aim.weight, 0, Time.deltaTime * 7);
            secondhand.weight = Mathf.Lerp(secondhand.weight, 0, Time.deltaTime * 7);
        }
    }

}
