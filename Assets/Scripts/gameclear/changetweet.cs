using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class changetweet : MonoBehaviour
{
    public GameObject[] setting;
    public GameObject error;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        error.GetComponent<Text>().enabled = false;
       
            setting[0].SetActive(false);

   
            setting[1].SetActive(true);
    }
    public void finishTwoStep()
    {
        SceneManager.UnloadScene("tweet");
        Resources.UnloadUnusedAssets();
    }
}
