using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScripts : MonoBehaviour
{
    ParticleSystem p;
    // Start is called before the first frame update
    void Start()
    {
        p = transform.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (p.isStopped)
            Destroy(gameObject);
    }
}
