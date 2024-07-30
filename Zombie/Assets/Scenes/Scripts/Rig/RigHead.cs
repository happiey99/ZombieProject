using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigHead : MonoBehaviour
{
    Rig rig;

    //Transform cameraAim;
    
    public Transform aimTarget;

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
        HeadAim();
        AimRig();
    }

    float maxAngle = 80.0f;

    void HeadAim()
    {
        Vector3 delta = aimTarget.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, delta);

        if (angle > maxAngle)
        {
            head.weight = Mathf.Lerp(head.weight, 0, Time.deltaTime * 7);
        }
        else
        {
            head.weight = Mathf.Lerp(head.weight, 1, Time.deltaTime * 7);
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
