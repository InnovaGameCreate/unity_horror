﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerLife : MonoBehaviour {

    static private int lifevalue = 3;
    static private bool  flag;

    //どのタイミングでLIFEを初期化するか
    public void set_flag(bool num) {
        flag = num;
        }
    // Use this for initialization
    void Start () {
        if(flag==false)
        lifevalue = 3;
    }
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text =  lifevalue.ToString();
        
    }

    void Awake()
	{ 
 		 
 	
 	}

    //ライフの減少とそれに伴うシーン処理
public void minus_life()
    {

        if (--lifevalue < 1)
        {
            flag = false;
            SceneManager.LoadScene("gameover");           //ゲームオーバーシーンへ

        }
        else
        {
            flag = true;
            SceneManager.LoadScene("main"); //リスタート

        }
        Resources.UnloadUnusedAssets();

    }
}
