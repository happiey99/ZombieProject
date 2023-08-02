using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{

    [SerializeField] float speed = 50;
    [SerializeField] Transform spark;
    [SerializeField] Transform BulletMark;
    [SerializeField] Transform BloodMark;




    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "object")
        {
            ContactPoint contact = collision.contacts[0];

            Instantiate(spark, transform.position, Quaternion.LookRotation(transform.forward * -1));
            Instantiate(BulletMark, transform.position, Quaternion.FromToRotation(-Vector3.forward, contact.normal));

            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "zombie")
        {
            ContactPoint contact = collision.contacts[0];

            //Instantiate(spark, transform.position, Quaternion.LookRotation(transform.forward * -1));
            Instantiate(BloodMark, transform.position, Quaternion.FromToRotation(-Vector3.forward, contact.normal));

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Default")
        //{
            //Vector3 q = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

            //Instantiate(spark, transform.position, Quaternion.LookRotation(transform.forward * -1));
            //Transform point = Instantiate(BulletMark, transform.position, Quaternion.FromToRotation(Vector3.up, q));
            //Destroy(this.gameObject);
        //}
    }

}
