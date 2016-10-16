using UnityEngine;
using System.Collections;

public class flow_elevator : MonoBehaviour {
    public float speed = 3;    //上下移動スピード
    public int range = 5;    //上下移動の範囲  下から始まって上に動く

    private float iniposiy;
    private float dir=1;
    // Use this for initialization
    void Start () {
        iniposiy = GetComponent<Transform>().position.y;
 
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().Translate(0, speed * dir * Time.deltaTime, 0);

        if (iniposiy > GetComponent<Transform>().transform.position.y || GetComponent<Transform>().transform.position.y > iniposiy + range)
        {
            dir *= -1;
            Vector3 samp = transform.position;
            samp.y = Mathf.Clamp(samp.y, iniposiy, iniposiy + range);
            transform.position = samp;
        }
	}


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Transform>().Translate(0, dir * speed * Time.deltaTime, 0);
        }
    }

}
