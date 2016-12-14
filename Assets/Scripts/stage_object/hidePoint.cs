﻿using UnityEngine;
using System.Collections;

public class hidePoint : MonoBehaviour
{

    private bool is_hiding = false;

    private bool hitPlayer = false;
    private Collider samp;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hitPlayer == true)
            inputhid();
    }

    void inputhid()
    {
        if (samp.gameObject.GetComponent<heromove>() != null)
        {
            if (samp.gameObject.GetComponent<heromove>().lookedImg.get_lookedbyenemy() == 0)
                //隠れる
                if (is_hiding == false && Input.GetKeyUp(KeyCode.UpArrow))
                {

                    is_hiding = true;
                    samp.gameObject.GetComponent<heromove>().InvincibleMode();
                    Vector3 p = samp.gameObject.transform.position;
                    p = new Vector3(this.transform.position.x - 1, p.y, 3);
                    samp.gameObject.transform.position = p;
                    samp.gameObject.GetComponent<Animator>().SetFloat("Horizontal", 0);
                }
                //表に出る
                else if (is_hiding == true && Input.GetKeyUp(KeyCode.UpArrow))
                {
                    is_hiding = false;
                    samp.gameObject.GetComponent<heromove>().OnFinishedInvincibleMode();
                    Vector3 q = samp.gameObject.transform.position;
                    q = new Vector3(q.x, q.y, 0);
                    samp.gameObject.transform.position = q;

                }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hitPlayer = true;
            samp = other;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))

            hitPlayer = false;


    }
}
