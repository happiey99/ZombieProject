using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extention :MonoBehaviour
{

    public static T GetAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();

        if (component == null)
        {
            go.AddComponent<T>();
        }

        return component;
    }
}
