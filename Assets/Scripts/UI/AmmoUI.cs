using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] Text AmmoCount;

    CanvasGroup cg;
    float alpha;
    [SerializeField] float speed = 6;
    private void Start()
    {
        alpha = 0;
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AmmoCount.text = $"{Managers._charInfo.curAmmo}/{Managers._charInfo.curFullAmmo}";

        if (Managers._charState.isAim)
            SetAlpha(true);
        else
            SetAlpha(false);
    }

    void SetAlpha(bool invisible = true)
    {
        if (!invisible)
        {
            alpha = Mathf.Lerp(alpha, 0, Time.deltaTime * speed);
        }
        else
        {
            alpha = Mathf.Lerp(alpha, 1, Time.deltaTime * speed);
        }

        cg.alpha = alpha;
    }
}
