using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class start : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
       
        float screenRate = (float)720 / Screen.height;
        if (screenRate > 1) screenRate = 1;
        int width = (int)(Screen.width * screenRate);
        int height = (int)(Screen.height * screenRate);
        Screen.SetResolution(width, height, false,60);
      //  Screen.SetResolution(1280, 720, true, 60);

    }


    private float count;
    public GameObject dark;
    // Update is called once per frame
    void Update()
    {
        if (count == 0 && Input.GetKeyDown(KeyCode.Return))
        {
            count++;
            GetComponent<AudioSource>().Play();
        }

        if (count > 0)
        {
            count += Time.deltaTime;
            if (count > 2)
                dark.GetComponent<Image>().color = new Color(0, 0, 0, (count - 2));
         
        }

        if (count > 3)
        {

            if (SaveData.GetString("rankname") == ""/*|| SaveData.GetString("pass")==""*/)
                SceneManager.LoadScene("rankingname");
            else
            {
                SceneManager.LoadScene("stage_select");
                UserAuth.Instance.AnonymousLogin();
            }
        }

    }
}
