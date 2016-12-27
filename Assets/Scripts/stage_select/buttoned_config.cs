using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class buttoned_config : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        //プレイ画面中のescボタンからのメニュー表示
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
