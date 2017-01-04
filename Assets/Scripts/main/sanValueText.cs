﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sanValueText : MonoBehaviour
{
    private float san = 100;     //san値
    private float sanrealmax = 100; //補正がかかった状態のsan最大値
    private float sanmax = 100;  //san最大値

    public float san_plustime = 0.07f;   //san値増加時間間隔

    public playerLife life_info;
    public bool debugmode;   //死なない状態にするかどうか
    private float timeElapsed;

    private float sanautocount = 0;
    public const float sanautocountmax = 3;     //攻撃を受けてから自然回復するまでの時間

    private float nextbigminuscount = 0;
    private const float nextbigminussan = 2;    //大きくSAN値削れてから次に大きく削れるまでの時間
    private GameObject player;

    private AudioSource[] sound = new AudioSource[(int)KindOfSound.NoneSound];

    private enum KindOfSound
    {
        Damaged1,
        Damaged2,
        Damaged3,
        NoneSound
    }
    // Use this for initialization
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        for (int i = 0; i < (int)KindOfSound.NoneSound; i++)
            sound[i] = audioSources[i];
        san = sanrealmax = sanmax;
    }

    // Update is called once per frame
    void Update()
    {

        //SAN値の自然回復
        if (sanautocount > 0)
        {
            sanautocount -= Time.deltaTime;
        }
        else
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= san_plustime)
            {

                if (san < sanrealmax)
                    san = san < sanrealmax ? san + 1 : sanrealmax;
                timeElapsed = 0.0f;

            }
        }

        //SAN値が大きく削れるまでのカウント
        if (nextbigminuscount > 0)
            nextbigminuscount -= Time.deltaTime;


        //   this.GetComponent<Text>().text = "SAN値：" + ((int)san).ToString();


        ////煙を焚く
        //if (san<sanrealmax/2)
        //{
        //    if (SceneManager.GetSceneByName("danger").isLoaded == false)
        //    {
        //        SceneManager.LoadScene("danger", LoadSceneMode.Additive);
        //    }

        //}
        //else
        //{
        //    if (SceneManager.GetSceneByName("danger").isLoaded == true)
        //    {
        //        SceneManager.UnloadScene("danger");
        //        Resources.UnloadUnusedAssets();
        //    }
        //}

    }

    //san値のじわじわ減少
    public void minus_san(float minus)
    {
        if (debugmode)
            return;

        sanautocount = sanautocountmax;
        san = san > 0 ? san - minus : 0;

        //死んだ
        if (san < 1)
        {
            life_info.minus_life();
            //san = sanmax;
        }

    }

    //san値が大きく減少
    public void minusbig_san(float minus)
    {

        if (debugmode)
            return;

        if (nextbigminuscount <= 0)
        {
            int rand = Random.Range((int)KindOfSound.Damaged1, (int)KindOfSound.Damaged3 + 1);
            sound[rand].Play();
            san = san > 0 ? san - minus : 0;
            nextbigminuscount = nextbigminussan;

            if (san < 1)
            {
                life_info.minus_life();
                // san = sanmax;

            }

        }

    }

    //即死
    public void kill_san()
    {
        //collisionStayのダブり誤差のため-10
        minus_san(sanrealmax);
    }


    public float get_san()
    {
        return san;
    }

    public float get_sanmax()
    {
        return sanmax;
    }

    public void set_sanrealmax(float next)
    {
        sanrealmax = next;
    }
    public float get_sanrealmax()
    {
        return sanrealmax;
    }
}
