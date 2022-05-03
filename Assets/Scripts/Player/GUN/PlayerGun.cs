using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] public GameObject ShootPoint;
    [SerializeField] private PlayerController pc;
    [SerializeField] public Transform target;


    // Update is called once per frame
    void Update()
    {
        AimPoint();

    }
   
    public RaycastHit hit;
    Ray ray;
    void AimPoint()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

        ray = Camera.main.ScreenPointToRay(screenCenterPoint);
       

        if (Physics.Raycast(ray, out hit, 999f))
        {
            if (hit.collider.gameObject.layer != 3)
                target.position = hit.point;
        }
    }



    public void LookPoint()
    {
        Vector3 look = target.position - transform.position;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(look.x, 0, look.z)), Time.deltaTime * 6);
        //Debug.Log(new Vector3(target.position.x, 0, target.position.z).normalized);
    }
}
