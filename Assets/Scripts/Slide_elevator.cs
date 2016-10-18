using UnityEngine;
using System.Collections;

public class Slide_elevator : MonoBehaviour {
    public float speed = 3;    //左右移動スピード
    public int range = 5;    //左右移動の範囲  左から始まって右に動く

    private float iniposix;
    private float dir = 1;
    // Use this for initialization
    void Start () {
        iniposix = GetComponent<Transform>().position.x;
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().Translate(speed * dir * Time.deltaTime, 0 , 0);
        if (GetComponent<Transform>().position.x > iniposix + range || GetComponent<Transform>().position.x < iniposix)
        {
            dir *= -1;
            Vector3 samp = transform.position;
            samp.x = Mathf.Clamp(samp.x, iniposix, iniposix + range);
            transform.position = samp;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
                other.gameObject.GetComponent<Transform>().Translate((other.gameObject.GetComponent<heromove>().get_face()>0?-1:1)*dir * speed*Time.deltaTime,0, 0);
        }
    }
}
