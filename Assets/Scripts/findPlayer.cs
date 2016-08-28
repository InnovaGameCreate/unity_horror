using UnityEngine;
using System.Collections;

public class findPlayer : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            transform.root.gameObject.GetComponent<Enemymove>().findPlayer = true;
    }
}
