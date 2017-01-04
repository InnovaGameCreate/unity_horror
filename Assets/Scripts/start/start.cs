using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class start : MonoBehaviour
{
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
