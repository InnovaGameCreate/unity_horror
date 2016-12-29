using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class rankingmanager : MonoBehaviour
{
    //どのシーンからやってきたか
    public static int fromscene;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (fromscene == 0)
                SceneManager.LoadScene("stage_select");
            else
                SceneManager.LoadScene("gameclear");
        }
    }
}
