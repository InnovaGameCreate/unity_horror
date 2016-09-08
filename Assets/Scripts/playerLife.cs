using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerLife : MonoBehaviour {

    private static int lifevalue=3;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = "LIFE：" + lifevalue.ToString();
        
    }

    void Awake()
	{ 
 		 
 	
 	}

public void minus_life()
    {
       
          
       
        
        if (--lifevalue < 1)
            ;//ゲームオーバーシーンへ
        else       
            SceneManager.LoadScene("main"); //リスタート

        Resources.UnloadUnusedAssets();

    }
}
