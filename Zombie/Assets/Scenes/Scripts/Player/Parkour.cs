using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Parkour : MonoBehaviour
{
    PlayerController pc;
    Animator animator;
    // Start is called before the first frame update
    public void Init()
    {
        animator = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        RayCast();
        Grab();
    }


    Ray ray;
    Ray lagRay;
    Ray headRay;

    float ladderSpeed;
    void RayCast()
    {
        lagRay = new Ray(transform.position, transform.forward * 0.1f);
        ray = new Ray(transform.position + new Vector3(0, 0.7f, 0), transform.forward);
        headRay = new Ray(transform.position + new Vector3(0, 1.5f, 0), transform.forward);

        LayerMask layerMask = LayerMask.GetMask("Ladder");

        RaycastHit ladder;


        bool hit = Physics.Raycast(ray, out ladder, 0.5f, layerMask);
        bool headHit = Physics.Raycast(headRay, out ladder, 1f, layerMask);
        bool lagHit = Physics.Raycast(lagRay, out ladder, 1f, layerMask);

        if (hit)
        {
            float vertical = Input.GetAxis("Vertical");

            if (vertical > 0 && !pc.ani._isLadder)
            {
                StartCoroutine(LadderStartUp(ladder));
            }

            if (pc.ani._isLadder)
            {
                if (vertical > 0)
                {
                    ladderSpeed = Mathf.Lerp(ladderSpeed, 1, Time.deltaTime * 7);

                    if (!headHit)
                    {
                        StartCoroutine(LadderFinishUp());
                    }

                }
                else if (vertical < 0)
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        ladderSpeed = Mathf.Lerp(ladderSpeed, -2, Time.deltaTime * 7);
                    }
                    else
                    {
                        ladderSpeed = Mathf.Lerp(ladderSpeed, -1, Time.deltaTime * 7);
                    }

                    if (pc.ani._isGround)
                    {
                        pc.ani._isLadder = false;
                    }
                }
                else
                {
                    ladderSpeed = Mathf.Lerp(ladderSpeed, 0, Time.deltaTime * 7);
                }

                Vector3 vector = new Vector3(0, ladderSpeed, 0);

                if (pc.cc.enabled)
                    pc.cc.Move(vector * Time.deltaTime);

            }

            if (pc.ani._isGround)
                pc.ani._ladderSpeed = 0;

            pc.ani._ladderSpeed = ladderSpeed;
        }

        if (lagHit && !hit && !headHit && !pc.ani._isLadder)
        {
            StartCoroutine(LadderStartDown(ladder));
        }
    }

    void Grab()
    {
        LayerMask layerMask = LayerMask.GetMask("CanGrab");
        RaycastHit hit;

        bool headHit = Physics.Raycast(headRay, out hit, 0.5f, layerMask);

        if (headHit)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !pc.ani.isGrab && !pc.ani.isHang)
            {
                StartCoroutine(GrabBar(hit));
            }

        }

        if (pc.ani.isGrab && pc.ani.isHang)
        {
            if (Input.GetKey(KeyCode.W))
            {
                pc.ani.GrabValue = 1;
                StartCoroutine(GrabUp());
            }
            else if (Input.GetKey(KeyCode.S))
            {
                pc.ani.GrabValue = -1;
                StartCoroutine(GrabDown());
            }

        }
    }

    IEnumerator GrabDown()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("WallPoints_Idle"))
        {
            yield return new WaitForSeconds(0.2f);
            pc.ani.isGrab = false;
            yield return Extention.DelayAnimation(animator);
            pc.ani.GrabValue = 0;
            pc.ani.isHang = false;
            yield return null;
        }
    }
    IEnumerator GrabUp()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("WallPoints_Idle"))
        {

            yield return Extention.DelayAnimation(animator);

            pc.ani.isGrab = false;
            pc.ani.GrabValue = 0;
            pc.ani.isHang = false;

            yield return null;
        }

    }

    IEnumerator GrabBar(RaycastHit hit)
    {
        pc.ani.isGrab = true;
        pc.cc.enabled = false;


        Vector3 vector =
            new Vector3(transform.position.x, hit.transform.position.y - 1.6f, transform.position.z);

        Vector3 vectorForward = hit.transform.forward;

        StartCoroutine(Extention.SetForward(transform, vectorForward));

        /* yield return */
        StartCoroutine(Extention.SetPosition(transform, vector));
        yield return StartCoroutine(Extention.DelayAnimation(animator));

        pc.cc.enabled = true;
        pc.ani.isHang = true;


        yield return null;
    }


    IEnumerator LadderStartDown(RaycastHit hit)
    {
        pc.cc.enabled = false;

        Vector3 ladderV = new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);
        Vector3 vector = ladderV + hit.transform.forward * 0.5f + hit.transform.right * -0.2f;

        pc.ani._isLadder = true;
        StartCoroutine(Extention.SetForward(transform, hit.transform.forward * -1, 0.1f));
        yield return StartCoroutine(Extention.SetPosition(transform, vector, 0.1f));


        pc.ani.LadderD = true;

        yield return StartCoroutine(Extention.DelayAnimation(animator));

        pc.ani.LadderD = false;

        pc.cc.enabled = true;

        yield return null;
    }

    IEnumerator LadderStartUp(RaycastHit hit)
    {
        pc.cc.enabled = false;

        Vector3 ladderV =
            new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);

        Vector3 vector = ladderV + hit.transform.forward * -0.35f;

        StartCoroutine(Extention.SetForward(transform, hit.transform.forward, 0.1f));
        yield return StartCoroutine(Extention.SetPosition(transform, vector, 0.1f));


        pc.cc.enabled = true;
        pc.ani._isLadder = true;
        pc.ani.LadderS = true;

        yield return new WaitForSeconds(0.1f);
        pc.ani.LadderS = false;

    }

    IEnumerator LadderFinishUp()
    {
        pc.ani._ladderSpeed = 0.0f;

        if (!pc.animator)
            pc.animator = pc.ani.ani;

        pc.cc.enabled = false;

        pc.ani.LadderU = true;

        yield return StartCoroutine(Extention.DelayAnimation(animator));

        Vector3 vector = transform.position + transform.forward / 10;

        pc.ani.LadderU = false;

        yield return StartCoroutine(Extention.SetPosition(transform, vector, 0.1f));

        pc.ani._isLadder = false;

        pc.cc.enabled = true;

        yield return null;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(ray);
    //    Gizmos.DrawRay(lagRay);
    //    Gizmos.DrawRay(headRay);
    //}

}
