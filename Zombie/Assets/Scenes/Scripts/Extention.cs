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

    public static IEnumerator SetPosition(Transform player, Vector3 vector, float time = 0.1f)
    {
        float elaps = 0.0f;

        while (elaps < time)
        {
            elaps += Time.deltaTime;
            player.position = Vector3.Lerp(player.position, vector, elaps / time);
            yield return null;
        }

        yield return null;
    }

    public static IEnumerator SetForward(Transform player, Vector3 vector, float time = 0.1f)
    {
        float elaps = 0.0f;

        while (elaps < time)
        {
            elaps += Time.deltaTime;
            player.transform.forward = Vector3.Lerp(player.forward, vector, elaps / time);
            yield return null;
        }

        yield return null;
    }
   
    public static IEnumerator DelayAnimation(Animator ani, int layer = 0)
    { 
        yield return new WaitForSeconds(0.01f);
        float curAnimationTime = ani.GetCurrentAnimatorClipInfo(layer).Length;
        yield return new WaitForSeconds(curAnimationTime);
    }
}
