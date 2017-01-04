using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class buttoned_config : MonoBehaviour {



    // Update is called once per frame
    void Update()
    {
        //プレイ画面中のescボタンからのメニュー表示
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<AudioSource>().Play();
            if (SceneManager.GetSceneByName("configmenu").isLoaded == false)
            {
                SceneManager.LoadScene("configmenu", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadScene("configmenu");
                Resources.UnloadUnusedAssets();
            }
        }
    }
}
