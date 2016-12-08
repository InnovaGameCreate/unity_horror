using UnityEngine;
using System.Collections;

public class Slide_elevator : MonoBehaviour {
    public float speed = 3;    //左右移動スピード
    public int range = 5;    //左右移動の範囲  左から始まって右に動く

    private float iniposix;
    private float dir = 1;

    private bool onplayer;
    private GameObject player;
    private bool outrange;

    // Use this for initialization
    void Start () {
        iniposix = GetComponent<Transform>().position.x;
    }
	
	// Update is called once per frame
	void Update () {
            Vector3 posi = GetComponent<Transform>().position;
        if (outrange == false && (GetComponent<Transform>().position.x > iniposix + range || GetComponent<Transform>().position.x < iniposix))
        {
            dir *= -1;
            outrange = true;
        }
        else
            outrange = false;

        posi.x += speed * dir * Time.deltaTime;
        GetComponent<Transform>().position = posi;

        if (onplayer)
            player.GetComponent<Transform>().Translate((player.GetComponent<heromove>().get_face()> 0 ? -1 : 1) * dir * speed * Time.deltaTime, 0, 0);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player == null)
                    player = other.gameObject;
            onplayer = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))

            onplayer = false;
        
    }
}
