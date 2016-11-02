using UnityEngine;
using System.Collections;

public class findPlayer : MonoBehaviour
{
    private heromove state_info;      //必ずプレイヤーを指定すること

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state_info = other.gameObject.GetComponent<heromove>();
            transform.parent.GetComponent<enemyBase>().set_targetplayer(other.gameObject);
        }
    }
    //索敵範囲内 敵を見つける
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (transform.parent.gameObject.GetComponent<enemyBase>().chaseplayer == true)
            {
                if (transform.parent.GetComponent<enemyBase>().get_disappear_flag() == false)
                    transform.parent.gameObject.GetComponent<enemyBase>().set_findPlayer(true);
                if (state_info != null)
                    if (state_info.get_state() == heromove.State.Invincible)
                        transform.parent.gameObject.GetComponent<enemyBase>().set_findPlayer(false);

            }
        }
    }
}
