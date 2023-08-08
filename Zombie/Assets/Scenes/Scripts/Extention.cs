using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extention 
{

    public static T GetAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();

        if (component == null)
        {
           component = go.AddComponent<T>();
        }
        
        return component;
    }
}
