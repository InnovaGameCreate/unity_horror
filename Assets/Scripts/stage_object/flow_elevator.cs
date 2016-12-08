using UnityEngine;
using System.Collections;

public class flow_elevator : MonoBehaviour
{
    public float speed = 3;    //上下移動スピード
    public int range = 5;    //上下移動の範囲  下から始まって上に動く

    private float iniposiy;
    private float dir = 1;
    // Use this for initialization
    private bool onplayer;
    private GameObject player;
    private bool outrange;

    void Start()
    {
        iniposiy = GetComponent<Transform>().position.y;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posi = GetComponent<Transform>().position;

        if (outrange==false&&(iniposiy > GetComponent<Transform>().transform.position.y || GetComponent<Transform>().transform.position.y > iniposiy + range))
        {
            dir *= -1;
            outrange = true;
        }
        else
            outrange = false;
       
        posi.y += speed * dir * Time.deltaTime;
        GetComponent<Transform>().position = posi;
        
        if (onplayer)
            player.gameObject.GetComponent<Transform>().Translate(0, dir * speed * Time.deltaTime, 0);
    }


    void OnTriggerStay(Collider other)
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
