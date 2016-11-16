using UnityEngine;
using System.Collections;

public class ladderPoint : MonoBehaviour
{

    GameObject player;

    private heromove move_info;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //はしご移動
    public void upMove(float y, float upper, float under)
    {
        move_info.set_is_ground(true);
        player.GetComponent<Animator>().SetBool("isGround", move_info.get_is_ground());


        Vector3 p = player.transform.position;
        p = new Vector3(p.x, p.y + y * move_info.normalspeed * Time.deltaTime, p.z);
        p.y = p.y < under ? under : (p.y > upper) ? upper : p.y;
        player.transform.position = p;

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player == null)
            {
                player = other.gameObject;
                move_info = player.GetComponent<heromove>();
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                upMove(Input.GetAxis("Vertical"), this.transform.position.y + (float)this.transform.localScale.y, this.transform.position.y - (float)this.transform.localScale.y + 1.5f);
                //  はしごに接触してる間物理処理に制限をかける
                player.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY);
                //はしごから離れたらプレイヤーの状態を上る状態に
                move_info.set_state(heromove.State.Upping);
                player.GetComponent<Animator>().SetBool("islad", true);
            }
        }
    }

    //はしごから離れたらプレイヤーの状態をノーマルに
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            move_info.set_state(heromove.State.Normal);
            player.GetComponent<Animator>().SetBool("islad", false);
            //はしご系から離れた時の物理処理の初期化
            player.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ);

        }
    }
}
