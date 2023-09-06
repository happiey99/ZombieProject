using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class LadderSystem : MonoBehaviour
{
    PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        RayCast();
    }


    Ray lagRay;
    Ray ray;
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
                {
                    pc.ani._ladderSpeed = ladderSpeed;
                    pc.cc.Move(vector * Time.deltaTime);
                }
            }
        }

        if (lagHit && !headHit && !headHit && !pc.ani._isLadder)
        {
            StartCoroutine(LadderStartDown(ladder));
        }
    }
    IEnumerator LadderStartDown(RaycastHit hit)
    {//½Ã¹ß
        pc.ani.LadderD = true;
        pc.cc.enabled = false;

        Vector3 vector =
            new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);

        transform.position = vector;
        //transform.forward = hit.transform.forward;

        yield return StartCoroutine(DelayAnimation());

        pc.ani._isLadder = true;
        pc.ani.LadderD = false;
        pc.cc.enabled = true;
        yield return null;
    }

    IEnumerator LadderStartUp(RaycastHit hit)
    {
        pc.cc.enabled = false;

        Vector3 vector = hit.transform.position + hit.transform.forward * -0.35f;

        yield return StartCoroutine(SetPosTime(hit.transform, vector));
        pc.cc.enabled = true;
        pc.ani._isLadder = true;
        yield return null;
    }

    IEnumerator LadderFinishUp()
    {
        pc.ani._ladderSpeed = 0.0f;

        if (!pc.animator)
            pc.animator = pc.ani.ani;

        pc.cc.enabled = false;

        pc.ani.LadderU = true;

        yield return StartCoroutine(DelayAnimation());

        Vector3 vector = transform.position + transform.forward / 10;

        pc.ani.LadderU = false;

        yield return StartCoroutine(SetPosTime(transform, vector));

        pc.ani._isLadder = false;

        pc.cc.enabled = true;

        yield return null;
    }


    float time = 0.1f;

    IEnumerator SetPosTime(Transform t, Vector3 vector)
    {
        float elaps = 0.0f;

        while (elaps < time)
        {
            elaps += Time.deltaTime;

            transform.forward = Vector3.Lerp(transform.forward, t.forward, elaps / time);
            transform.position = Vector3.Lerp(transform.position, vector, elaps / time);

            yield return null;
        }

        yield return null;
    }
    IEnumerator DelayAnimation()
    {
        yield return new WaitForSeconds(0.01f);
        float curAnimationTime = pc.animator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(curAnimationTime);
    }

}
