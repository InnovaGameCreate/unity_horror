using UnityEngine;
using System.Collections;

public class chasePlayer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    //追跡範囲内 ※Updateと OnTriggerStay関連ではフレームレートが異なる
    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (transform.root.gameObject.GetComponent<groundEnemy>() != null)
            {
                if (transform.root.gameObject.GetComponent<groundEnemy>().get_findPlayer() == true)
                    transform.root.gameObject.GetComponent<groundEnemy>().chasePlayer(other);
            }
            else if (transform.root.gameObject.GetComponent<flyEnemy>().get_findPlayer() == true)
                transform.root.gameObject.GetComponent<flyEnemy>().chasePlayer(other);
        }

    }

    //追跡範囲外
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (transform.root.gameObject.GetComponent<groundEnemy>() != null)
            
                transform.root.gameObject.GetComponent<groundEnemy>().set_findPlayer(false);
            else

                transform.root.gameObject.GetComponent<flyEnemy>().set_findPlayer(false);
        }
    }

}