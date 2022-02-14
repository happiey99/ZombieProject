using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController CC;

    [SerializeField] private float Speed = 6.0f;
    
    Vector3 PlayerCurFront;

    private Vector3 curVelocity;
    
    void Awake()
    {
        CC = GetComponent<CharacterController>();
    }

    void Update()
    {
        CurFront();
        PlayerMove();
    }

    void PlayerMove()
    {
        if (!Input.anyKey)
            return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = new Vector3(PlayerCurFront.x,0, PlayerCurFront.z);
        
        CC.Move(move * Speed * Time.deltaTime); 
    }

    void CurFront()
    {
        PlayerCurFront = (transform.position - Camera.main.transform.position).normalized;
        transform.forward = new Vector3(PlayerCurFront.x,0,PlayerCurFront.z);
    }
}
