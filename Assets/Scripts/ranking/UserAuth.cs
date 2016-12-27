using UnityEngine;
using System;
using System.Collections;
using NCMB;

public class UserAuth : SingletonMonoBehaviour<UserAuth>
{
   public string currentPlayerName;
    // パスワードは適当に設定
    static string PASSWORD = "zwDyWpnKZx74xdayyhs34s";

    public void AnonymousLogin()
    {
        string id = SaveData.GetString("rankname");// UUIDManager.Instance.uuid;
        string pw = PASSWORD;
        NCMBUser.LogInAsync(id, pw, (NCMBException e) => {
            if (e == null)
            {
                currentPlayerName = id;
                Debug.Log("anonymous login");
            }
            else if (e.ErrorCode == NCMBException.INCORRECT_PASSWORD)
            {
                // ユーザーがDBに登録されていない場合は登録する
                AnonymousSignup();
            }
        });
    }

    public void AnonymousSignup()
    {
        NCMBUser user = new NCMBUser();
        user.UserName = SaveData.GetString("rankname"); ;//UUIDManager.Instance.uuid;
        user.Password = PASSWORD;
        NCMBACL acl = new NCMBACL();
        acl.SetWriteAccess("*", true);
        acl.SetReadAccess("*", true);
        user.ACL = acl;
        user.SignUpAsync((NCMBException e) => {
            if (e == null)
            {
                Debug.Log("anonymous signup");
                currentPlayerName= user.UserName;
            }
  
        });
    }

    public void Logout()
    {
        NCMBUser.LogOutAsync((NCMBException e) => {
            if (e == null)
            {
                currentPlayerName = null;
                Debug.Log("logout");
            }
        });
    }
    // 現在のプレイヤー名を返す --------------------
    public string currentPlayer()
    {
        return currentPlayerName;
    }

    // シングルトン化する ------------------------

    private UserAuth instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            string name = gameObject.name;
            gameObject.name = name + "(Singleton)";

            GameObject duplicater = GameObject.Find(name);
            if (duplicater != null)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.name = name;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
