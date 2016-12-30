using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;

public class TwitterComponentHandler : MonoBehaviour
{

    public GameObject inputPINField;
    public GameObject inputTweetField;

    public changetweet cha;

    private const string CONSUMER_KEY = "Ln1nWOhuqw4pnVy9YHFj1g8ui";
    private const string CONSUMER_SECRET = "Ug9JhS8IqIhpWiuOdhzFroSI7LjfcfRvi2mIp05ZfPu5ovF5Li";

    Twitter.RequestTokenResponse m_RequestTokenResponse;
    Twitter.AccessTokenResponse m_AccessTokenResponse;

    const string PLAYER_PREFS_TWITTER_USER_ID = "TwitterUserID";
    const string PLAYER_PREFS_TWITTER_USER_SCREEN_NAME = "TwitterUserScreenName";
    const string PLAYER_PREFS_TWITTER_USER_TOKEN = "TwitterUserToken";
    const string PLAYER_PREFS_TWITTER_USER_TOKEN_SECRET = "TwitterUserTokenSecret";

    const string PLAYER_PREFS_TWITTER_TWEETED_IDS = "TwitterTweetedIDs";

    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }


    /* OnClick Event */


    public void OnClickGetPINButon()
    {
        cha.firsterrornone();
        StartCoroutine(Twitter.API.GetRequestToken(CONSUMER_KEY, CONSUMER_SECRET,
            new Twitter.RequestTokenCallback(this.OnRequestTokenCallback)));
    }

    public void OnClickAuthPINButon()
    {
        string myPIN = inputPINField.GetComponent<InputField>().text;

        StartCoroutine(Twitter.API.GetAccessToken(CONSUMER_KEY, CONSUMER_SECRET, m_RequestTokenResponse.Token, myPIN,
            new Twitter.AccessTokenCallback(this.OnAccessTokenCallback)));
    }

    public void OnClickTweetButon()
    {
        string myTweet = inputTweetField.GetComponent<InputField>().text;

        StartCoroutine(Twitter.API.PostTweet(myTweet, CONSUMER_KEY, CONSUMER_SECRET, m_AccessTokenResponse,
            new Twitter.PostTweetCallback(this.OnPostTweet)));
    }
    /* Callback Event */


    void OnRequestTokenCallback(bool success, Twitter.RequestTokenResponse response)
    {
        if (success)
        {
            string log = "OnRequestTokenCallback - succeeded";
            log += "\n    Token : " + response.Token;
            log += "\n    TokenSecret : " + response.TokenSecret;
            print(log);

            m_RequestTokenResponse = response;

            print(response.Token);
            print(response.TokenSecret);

            Twitter.API.OpenAuthorizationPage(response.Token);
        }
        else
        {
            print("OnRequestTokenCallback - failed.");
        }
    }

   
    public void LoadTwitterUserInfo()
    {
        m_AccessTokenResponse = new Twitter.AccessTokenResponse();

        m_AccessTokenResponse.UserId = SaveData.GetString(PLAYER_PREFS_TWITTER_USER_ID);
        m_AccessTokenResponse.ScreenName = SaveData.GetString(PLAYER_PREFS_TWITTER_USER_SCREEN_NAME);
        m_AccessTokenResponse.Token = SaveData.GetString(PLAYER_PREFS_TWITTER_USER_TOKEN);
        m_AccessTokenResponse.TokenSecret = SaveData.GetString(PLAYER_PREFS_TWITTER_USER_TOKEN_SECRET);

        if (!string.IsNullOrEmpty(m_AccessTokenResponse.Token) &&
            !string.IsNullOrEmpty(m_AccessTokenResponse.ScreenName) &&
            !string.IsNullOrEmpty(m_AccessTokenResponse.Token) &&
            !string.IsNullOrEmpty(m_AccessTokenResponse.TokenSecret))
        {
            string log = "LoadTwitterUserInfo - succeeded";
            log += "\n    UserId : " + m_AccessTokenResponse.UserId;
            log += "\n    ScreenName : " + m_AccessTokenResponse.ScreenName;
            log += "\n    Token : " + m_AccessTokenResponse.Token;
            log += "\n    TokenSecret : " + m_AccessTokenResponse.TokenSecret;
            print(log);
        }
    }

    public void resetdata()
    {
        SaveData.SetString(PLAYER_PREFS_TWITTER_USER_ID, "");
        SaveData.SetString(PLAYER_PREFS_TWITTER_USER_SCREEN_NAME, "");
        SaveData.SetString(PLAYER_PREFS_TWITTER_USER_TOKEN, "");
        SaveData.SetString(PLAYER_PREFS_TWITTER_USER_TOKEN_SECRET, "");
    }
    void OnAccessTokenCallback(bool success, Twitter.AccessTokenResponse response)
    {
        if (success)
        {
           
            string log = "OnAccessTokenCallback - succeeded";
            log += "\n    UserId : " + response.UserId;
            log += "\n    ScreenName : " + response.ScreenName;
            log += "\n    Token : " + response.Token;
            log += "\n    TokenSecret : " + response.TokenSecret;
            print(log);

            m_AccessTokenResponse = response;
    
            SaveData.SetString(PLAYER_PREFS_TWITTER_USER_ID, response.UserId);
            SaveData.SetString(PLAYER_PREFS_TWITTER_USER_SCREEN_NAME, response.ScreenName);
            SaveData.SetString(PLAYER_PREFS_TWITTER_USER_TOKEN, response.Token);
            SaveData.SetString(PLAYER_PREFS_TWITTER_USER_TOKEN_SECRET, response.TokenSecret);
            cha.finishOneStep();
        }
        else
        {
            cha.firsterror();
            print("OnAccessTokenCallback - failed.");
        }
    }

    void OnPostTweet(bool success, string response)
    {
        print("OnPostTweet - " + (success ? "succedded." : "failed."));

        if (success)
        {
            cha.finishTwoStep();
            var json = JSON.Parse(response);

            print(json["id"]);
        }
    }



}