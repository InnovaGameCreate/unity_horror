using UnityEngine;
using System.Collections;

public class hidespace : MonoBehaviour {

    Collider samp;
    // Use this for initialization
    void Start () {
	
	}
	


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
                other.gameObject.GetComponent<heromove>().InvincibleMoveMode();
        
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
                other.gameObject.GetComponent<heromove>().OnFinishedInvincibleMode();

    }
}

