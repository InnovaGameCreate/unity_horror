using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class buttoned_exit : MonoBehaviour {

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetSceneByName("main_escmenu").isLoaded == false)          
           {
                SceneManager.LoadScene("main_escmenu", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadScene("main_escmenu");
                Resources.UnloadUnusedAssets();
            }
        }
	}
  
   
}
