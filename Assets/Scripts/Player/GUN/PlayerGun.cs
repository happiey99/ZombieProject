using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] public GameObject ShootPoint;
    [SerializeField] private PlayerController pc;
    [SerializeField] public Transform target;

    [SerializeField] GameObject ItemSet;
    [SerializeField] TextMeshProUGUI text;
    //[SerializeField]

   
    private void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        AimPoint();
        PickUpThings();
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


    public void PickUpThings()
    {
        if (hit.transform.tag == "Item")
        {
            float v = (transform.position - hit.transform.position).magnitude;

            if (v < 2)
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!Managers._inven.BagIsFull())
                    {
                        Managers._charState.GrapItem = true;
                        Managers._inven.GetItem(hit.transform.gameObject.name);
                        StartCoroutine(SystemMessage.intance.SetMessage($"æ∆¿Ã≈∆ <color=#00ff00ff> {hit.transform.gameObject.name} </color> ¿ª »πµÊ«ﬁΩ¿¥œ¥Ÿ!"));
                      
                        Destroy(hit.transform.gameObject);
                    }
                    else
                    {
                        StartCoroutine(SystemMessage.intance.SetMessage("∞°πÊ¿Ã ∞°µÊ ¬˜¿÷Ω¿¥œ¥Ÿ!"));
                        return;
                    }
                }

                ItemSet.SetActive(true);
                text.text = $"{hit.transform.gameObject.name}";
            }
            else
            {
                ItemSet.SetActive(false);
            }

        }
        else
        {
            ItemSet.SetActive(false);
        }


    }
}
