using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int hp = 100;
    string[] hit_motion = new string[4];

    private Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
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

    public void Hit(int _damage, Transform playerPos)
    {
        int r = Random.Range(0, 4);
        hp -= _damage;
        Vector3 dir = transform.position - playerPos.position;
        ani.Play($"{hit_motion[r]}");
        transform.forward = dir * -1;
    }
}
