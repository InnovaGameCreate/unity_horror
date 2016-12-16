using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class changetweet : MonoBehaviour {
   public GameObject[]setting;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
   public void finishOneStep()
    {
        for (int i = 0; i < 3; i++)
            setting[i].SetActive(false);

        for (int i = 3; i < 5; i++)
            setting[i].SetActive(true);
    }
    public void finishTwoStep()
    {
        SceneManager.UnloadScene("tweet");
        Resources.UnloadUnusedAssets();
    }
}
