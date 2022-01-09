using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController Cc;
    float gravity = -20;
    float jumpHight = 6;
    Vector3 velocity;

    public Transform groundCheck;
   public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    // Start is called before the first frame update
    void Awake()
    {
        velocity = Vector3.zero;
        Cc = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }
       
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        Cc.Move(move*6*Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2 * gravity);
        }
       
        velocity.y += gravity * Time.deltaTime; 
        Cc.Move(velocity * Time.deltaTime);
    }
}
