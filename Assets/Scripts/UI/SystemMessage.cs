using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemMessage : MonoBehaviour
{
    [SerializeField] GameObject message;

  public  static SystemMessage intance;

    //tart is called before the first frame update
    void Start()
    {
        if (intance == null)
        {
            intance = this;
        }

    }

    public IEnumerator SetMessage(string text)
    {
        float delta = 0.0f;

        GameObject go = Instantiate(message, transform);

        Text t = go.GetComponent<Text>();
        CanvasGroup cg = go.GetComponent<CanvasGroup>();
        t.text = text;

        while (delta < 1f)
        {
            if (delta > 0.3f)
                cg.alpha -= Time.deltaTime * 1f;

            t.rectTransform.position += new Vector3(0, 1);

            delta += Time.deltaTime * 0.8f;
            yield return null;
        }

        Destroy(go);

    }
}
