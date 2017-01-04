﻿using UnityEngine;
using System.Collections;


public class jump : MonoBehaviour
{


    public float Player_JumpPower;

    //接触したらプレイヤーに上方向の力を与える
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * this.Player_JumpPower);
            collision.gameObject.GetComponent<heromove>().set_is_ground(false);
        }
    }
}

