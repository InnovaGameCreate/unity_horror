using UnityEngine;
using System.Collections;

public class LoginManager : MonoBehaviour
{
    public void Start()
    {
        // 起動時に匿名ログイン
        //  UserAuth.Instance.AnonymousSignup();
     
    }
    void OnApplicationQuit()
    {
        // アプリ終了時にログアウト
        UserAuth.Instance.Logout();
    }

}