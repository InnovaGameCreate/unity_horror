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

    void OnTriggerStay(Collider other)
    {
     
        if (other.CompareTag("Player") )
            if(transform.root.gameObject.GetComponent<Enemymove>().findPlayer == true)
            transform.root.gameObject.GetComponent<Enemymove>().chasePlayer(other);
    
    }
  

}