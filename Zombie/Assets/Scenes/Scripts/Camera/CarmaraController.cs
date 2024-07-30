using UnityEngine;

public class CarmaraController : MonoBehaviour
{

    public Transform player;

    float x;
    float y;


    private void LateUpdate()
    {
        CarmaraMove();
    }

    void CarmaraMove()
    {
        x += Input.GetAxis("Mouse X");
        y += Input.GetAxis("Mouse Y") * -1;

        y = Mathf.Clamp(y, -50, 50);

        player.rotation = Quaternion.Euler(new Vector3(player.rotation.x + y, player.rotation.y + x, 0));

    }


}
