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
     
        if (other.CompareTag("Player") )
            if(transform.root.gameObject.GetComponent<Enemymove>().get_findPlayer() == true)
            transform.root.gameObject.GetComponent<Enemymove>().chasePlayer(other);
    
    }

    //追跡範囲外
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            transform.root.gameObject.GetComponent<Enemymove>().set_findPlayer(false);
    }

}