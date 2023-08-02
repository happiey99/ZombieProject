using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class Fov : MonoBehaviour
{
    public enum ZombieType
    {
        Idle,
        NonIdle,
    }

    public ZombieType zType;

    NavMeshAgent nav;

    public float viewRadius;

    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public Vector3 walkPoint;
    public float walkPointRange;

    public bool isChasing;

    [HideInInspector]
    public bool isMove = false;
    public bool isRun = false;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        //StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    private void Update()
    {

        if (nav.velocity.magnitude > 0.2f)
        {
            isMove = true;
        }
        else
        {
            isRun = false;
            isMove = false;
        }

        if (!isChasing && zType == ZombieType.NonIdle)
        {
            Patrol();
        }

        FindVisibleTargets();

    }


    //IEnumerator FindTargetsWithDelay(float delay)
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(delay);

    //        FindVisibleTargets();

    //    }
    //}

    public bool walkPointSet = false;

    void Patrol()
    {
        if (!walkPointSet)
        {
            CheckWayPoint();
        }


        if (walkPointSet)
        {
            nav.stoppingDistance = 0;
            nav.speed = 1;
            nav.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1.7f)
        {
            walkPointSet = false;
        }
    }

    void CheckWayPoint()
    {
        float vectorX = Random.Range(-walkPointRange, walkPointRange);
        float vectorZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + vectorX, 0, transform.position.z + vectorZ);
        walkPointSet = true;
    }

    void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (targetsInViewRadius.Length == 0)
            isChasing = false;

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;


            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    nav.speed = 3;
                    nav.stoppingDistance = 1.5f;
                    isRun = true;

                    isChasing = true;

                    nav.SetDestination(target.position);
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegree, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegree += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegree * Mathf.Deg2Rad));
    }
}
