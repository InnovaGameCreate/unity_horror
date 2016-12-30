using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class changetweet : MonoBehaviour
{
    public GameObject[] setting;
    public GameObject error;
    public Text username;
    public InputField tweetext;
    public TwitterComponentHandler samp;
    const string PLAYER_PREFS_TWITTER_USER_SCREEN_NAME = "TwitterUserScreenName";
    // Use this for initialization
    void Start()
    {
        if (SaveData.GetString(PLAYER_PREFS_TWITTER_USER_SCREEN_NAME) == "")
        {
            setting[0].SetActive(true);
            setting[1].SetActive(false);
        }
        else
        {
          
            username.text = "ログイン名：" + SaveData.GetString(PLAYER_PREFS_TWITTER_USER_SCREEN_NAME);
            samp.LoadTwitterUserInfo();
            setting[0].SetActive(false);
            setting[1].SetActive(true);

            tweetext.text="ホラーゲーム「Sanity」 \nステージ"+(int)stage_select.stage_is+"をクリア \n名状しがたきものを見た回数：" +
                lookenemycount.get_lookcount()+"回 \nタイム："+
                 ((int)((int)timercount.usedtime / 60)).ToString() + "分" + ((int)timercount.usedtime % 60).ToString() + "秒 \n#Sanity";
      
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (setting[0].activeInHierarchy == true && Input.GetKeyDown(KeyCode.Return))
            samp.OnClickAuthPINButon();
        //else if (setting[1].activeInHierarchy == true && Input.GetKeyDown(KeyCode.Return))
        //    samp.OnClickTweetButon();
    }
    public void firsterror()
    {
        error.GetComponent<Text>().enabled = true;
    }
    public void firsterrornone()
    {
        error.GetComponent<Text>().enabled = false;
    }
    public void finishOneStep()
    {
        username.text = "ログイン名：" + SaveData.GetString(PLAYER_PREFS_TWITTER_USER_SCREEN_NAME);
        tweetext.text = "ホラーゲーム「Sanity」 \nステージ" + (int)stage_select.stage_is + "をクリア \n名状しがたきものを見た回数：" +
                lookenemycount.get_lookcount() + "回 \nタイム：" +
                 ((int)((int)timercount.usedtime / 60)).ToString() + "分" + ((int)timercount.usedtime % 60).ToString() + "秒 \n#Sanity";

        error.GetComponent<Text>().enabled = false;
            setting[0].SetActive(false);  
            setting[1].SetActive(true);
    }

    public void finishTwoStep()
    {
        SceneManager.UnloadScene("tweet");
        Resources.UnloadUnusedAssets();
    }

    public void logOut()
    {
        samp.resetdata();
        setting[0].SetActive(true);
        setting[1].SetActive(false);
    }
}
