﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class start : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (SaveData.GetString("rankname") == ""/*|| SaveData.GetString("pass")==""*/)
                SceneManager.LoadScene("rankingname");
            else
            {
                SceneManager.LoadScene("stage_select");
                UserAuth.Instance.AnonymousLogin();
            }
        }
    }
}
