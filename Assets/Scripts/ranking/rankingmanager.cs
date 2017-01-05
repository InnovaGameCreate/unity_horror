using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class rankingmanager : MonoBehaviour
{
    //どのシーンからやってきたか
    public static int fromscene;
    private float count;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (count==0&&Input.GetKeyDown(KeyCode.Return))
        {
            count++;
            GetComponent<AudioSource>().Play();

        }
        if (count > 0)
            count += Time.deltaTime;
        if (count > 1.1f)
        {
            if (fromscene == 0)
                SceneManager.LoadScene("stage_select");
            else
                SceneManager.LoadScene("gameclear");
        }

    }
}
