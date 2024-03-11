using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int hp = 10000;
    string[] hit_motion = new string[4];

    public float distance;

    public GameObject hit_GameObject;
    public GameObject blood_GameObject;
    
    private ParticleSystem hit_particle;
    private ParticleSystem blood_particle;
    
    private Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();

        hit_particle = hit_GameObject.GetComponent<ParticleSystem>();
        blood_particle = blood_GameObject.GetComponent<ParticleSystem>();
        
        hit_motion[0] = "GetHitLightLeftZombie";
        hit_motion[1] = "GetHitLightRightZombie";
        hit_motion[2] = "GetHitLightFrontZombie";
        hit_motion[3] = "GetHitLightBackZombie";

    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private Transform player;
    public void playerDistance(Transform _player)
    {
        player = _player;
        distance = (transform.position - _player.transform.position).magnitude;
    }
    public void Hit(int _damage)
    {
        int r = Random.Range(0, 4);
        hp -= _damage;
        ActiveParticle();
        StartCoroutine(Extention.CameraShake());
        Vector3 dir = transform.position - player.position;
        ani.Play($"{hit_motion[r]}");
        transform.forward = dir * -1;
    }

   
    void ActiveParticle()
    {
        hit_particle.Play();
        blood_particle.Play();
    }
}
