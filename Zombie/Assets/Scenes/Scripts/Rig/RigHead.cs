using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigHead : MonoBehaviour
{
    Rig rig;
    Transform aimTarget;
    CameraController cameraTrans;

    private void Start()
    {
        rig = GetComponent<Rig>();
        aimTarget = transform.GetChild(0).GetChild(0).transform;
        cameraTrans = transform.parent.GetComponentInChildren<CameraController>();
    }
    private void LateUpdate()
    {
        ClampAroundHead();
        AimPositioin();
    }

    void ClampAroundHead()
    {
        if (cameraTrans.transform.localEulerAngles.y <= 100 &&
            cameraTrans.transform.localEulerAngles.y >= -10 ||
            cameraTrans.transform.localEulerAngles.y <= 370 &&
            cameraTrans.transform.localEulerAngles.y >= 260)
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

}
