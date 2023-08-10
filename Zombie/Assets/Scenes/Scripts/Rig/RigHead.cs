using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigHead : MonoBehaviour
{
    Rig rig;


    Transform cameraAim;
    Transform aimTarget;


    private void Start()
    {
        rig = GetComponent<Rig>();


        cameraAim = Camera.main.transform.parent;
        aimTarget = cameraAim.transform.GetChild(0);
    }
    private void LateUpdate()
    {

    }

    //void ClampAroundHead()
    //{
    //    if (cameraTrans.transform.localEulerAngles.y <= 80 &&
    //        cameraTrans.transform.localEulerAngles.y >= 10 ||
    //        cameraTrans.transform.localEulerAngles.y <= 350 &&
    //        cameraTrans.transform.localEulerAngles.y >= 280)
    //    {
    //        rig.weight = Mathf.Lerp(rig.weight, 1, 0.1f);
    //    }
    //    else
    //    {
    //        rig.weight = Mathf.Lerp(rig.weight, 0, 0.1f );
    //    }

    //}


    //void AimPositioin()
    //{
    //    Vector2 midOfS = new Vector2(Screen.width / 2f, Screen.height / 2f);

    //    Ray ray = Camera.main.ScreenPointToRay(midOfS);

    //    RaycastHit[] hits = Physics.RaycastAll(ray);

    //    for (int i = 0; i < hits.Length; i++)
    //    {
    //        if (hits[i].transform.gameObject.layer != 6)
    //        {
    //            aimTarget.position = hits[i].point;
    //            break;
    //        }
    //    }
    //}

}
