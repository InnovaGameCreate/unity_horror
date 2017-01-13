﻿using UnityEngine;
using System.Collections;

public class deleteObject : MonoBehaviour {

    private sanValueText sanText; //外部のsanValueTexオブジェクトを見えるよう定義
                   
    void Start () {
        sanText = GameObject.Find("SANValueText").GetComponent<sanValueText>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        //プレイヤーならライフ減少
        if (other.CompareTag("Player"))
            sanText.kill_san();
    }
        void OnTriggerStay(Collider other)
    {

        //敵なら消滅
        if (other.CompareTag("Enemy"))
            Destroy(other.gameObject);

    }
}
