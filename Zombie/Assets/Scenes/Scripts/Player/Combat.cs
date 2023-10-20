using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Combat : MonoBehaviour
{
    public float coolTime = 0.8f;
    public float cur_Time = 0.0f;
    int attack_count = 0;


    Animator ani;


    IEnumerator reset;


    private void Start()
    {
        ani = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (reset != null)
                StopCoroutine(reset);

            attack_count++;
            cur_Time = 0;
            Debug.Log(attack_count);

            reset = ResetTimer();

            StartCoroutine(reset);
        }

    }

    IEnumerator ResetTimer()
    {
        while (coolTime >= cur_Time)
        {
            yield return null;

            cur_Time += Time.deltaTime;
        }

        attack_count = 0;
        Debug.Log("count is being: " + attack_count);

    }
}
