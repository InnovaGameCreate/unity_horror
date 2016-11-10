﻿using UnityEngine;
using System.Collections;

public class findPlayer : MonoBehaviour
{
    private heromove state_info;    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float rayn = transform.parent.GetComponent<enemyBase>().raynum;
        float range = transform.parent.GetComponent<enemyBase>().rayangle / 2;
        float length = transform.parent.GetComponent<enemyBase>().raylength;
        for (int i = 0; i < rayn ; i++)
        {
            
            RaycastHit hit;
            Debug.DrawRay(this.transform.position,  length* (transform.parent.GetComponent<enemyBase>().get_face() < 0 ? Quaternion.Euler(0f, 0f, -range + i * range *2 / (rayn - 1)) * Vector3.left : (transform.parent.GetComponent<enemyBase>().get_face() > 0 ? Quaternion.Euler(0f, 0f, -range + i * range * 2 / (rayn - 1)) * Vector3.right : Vector3.zero)), Color.red, 0, false);
            Ray ray = new Ray(transform.position, transform.parent.GetComponent<enemyBase>().get_face() > 0 ? Quaternion.Euler(0f, 0f, -range + i * range*2/(rayn-1)) * Vector3.left : Quaternion.Euler(0f, 0f, -range + i * range *2/ (rayn - 1)) * Vector3.right);
            if (Physics.Raycast(ray, out hit, length, transform.parent.GetComponent<enemyBase>().mask))
            {
                if (hit.collider.CompareTag("Player"))
                {

                    if (state_info == null)
                    {
                        state_info = hit.collider.gameObject.GetComponent<heromove>();
                        transform.parent.GetComponent<enemyBase>().set_targetplayer(hit.collider.gameObject);
                    }

                    if (transform.parent.gameObject.GetComponent<enemyBase>().chaseplayer == true)
                    {
                
                           transform.parent.gameObject.GetComponent<enemyBase>().set_findPlayer(true);


                    }

                }

            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && state_info == null)
        {
            //着地面にheromoveはアタッチされていないため
           // if (other.gameObject.GetComponent<heromove>() != null)
                state_info = other.gameObject.GetComponent<heromove>();

            transform.parent.GetComponent<enemyBase>().set_targetplayer(other.gameObject);
        }
    }
    //索敵範囲内 敵を見つける
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (transform.parent.gameObject.GetComponent<enemyBase>().chaseplayer == true)
            {


                if (state_info.get_state() == heromove.State.Invincible|| state_info.get_state() == heromove.State.InvincibleMove)
                {
                    transform.parent.gameObject.GetComponent<enemyBase>().set_findPlayer(false);
                }
                else if (transform.parent.GetComponent<enemyBase>().get_disappear_flag() == false)
                    transform.parent.gameObject.GetComponent<enemyBase>().set_findPlayer(true);


            }
        }
    }
}
