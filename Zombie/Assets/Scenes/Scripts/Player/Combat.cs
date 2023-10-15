using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Serialization;
using UnityEngine;

public class Combat : MonoBehaviour
{

    int combo_Stack = 0;

    float resetTime = 2.0f;

    private void Update()
    {
        
    }

    public void Combo()
    {
        if (Input.GetMouseButtonDown(0))
        {
            resetTime = 2.0f;
            combo_Stack++;
        }

        if(combo_Stack > 0)
        {
            resetTime -= Time.deltaTime;
        }

        if(resetTime <= 0)
        {
            resetTime = 2.0f;
            combo_Stack = 0;
        }

        Debug.Log(combo_Stack);
    }
}
