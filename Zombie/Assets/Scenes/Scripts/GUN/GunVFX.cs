using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunVFX : MonoBehaviour
{

    public ParticleSystem bullit_Shell;
    public ParticleSystem gun_Shot;


    // Start is called before the first frame update
    void Start()
    {
        StopParticle();
    }

    void StopParticle()
    {
        bullit_Shell.Stop();
        gun_Shot.Stop();
    }

    void PlayParticle(ParticleSystem p)
    {
        p.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayParticle(bullit_Shell);
            PlayParticle(gun_Shot);
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            StopParticle();
        }
    }
}
