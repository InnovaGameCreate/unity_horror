using UnityEngine; 
 using System.Collections;


public class jump : MonoBehaviour
{


    public float Player_JumpPower;
    public bool Jump;


    GameObject player;

    void Update()
    {



        player = GameObject.Find("Player");

    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * this.Player_JumpPower);

        }
    }
    void OnCollisionExit(Collision collision)
    {
        collision.gameObject.GetComponent<heromove>().set_is_ground(false);
    }
}
