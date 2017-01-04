using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class clearselect : MonoBehaviour
{


    private int selecting = 0;
    private int maxselect = 5;
    private float count;
    AudioSource[] sound = new AudioSource[2];
    // Use this for initialization
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        for (int i = 0; i < 2; i++)
            sound[i] = audioSources[i];

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetSceneByName("tweet").isLoaded == true)
            return;

        //escメニューの選択
        if (selecting < maxselect - 1 && Input.GetKeyDown(KeyCode.DownArrow))
        {
            sound[0].Play();
            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
            pos.y -= 80;
            GetComponent<RectTransform>().anchoredPosition = pos;
            selecting++;
        }
        else if (selecting > 0 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            sound[0].Play();
            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
            pos.y += 80;
            GetComponent<RectTransform>().anchoredPosition = pos;
            selecting--;
        }
        if (count==0&&Input.GetKeyDown(KeyCode.Return))
        {
            sound[1].Play();
            count++;
        }
        if (count > 0)
            count += Time.deltaTime;
        if (count > 1.1f)
        {
            switch (selecting)
            {
                case 0:
                    SceneManager.LoadScene(playerLife.scenename[heromove.nowstage]);
                    break;
                case 1:
                    if (heromove.nowstage < 5)
                        SceneManager.LoadScene(playerLife.scenename[heromove.nowstage + 1]);

                    break;
                case 2:
                    SceneManager.LoadScene("stage_select");
                    break;
                case 3:
                    if (UserAuth.Instance.currentPlayer() == "")
                    {


                    }
                    else
                    {
                        SceneManager.LoadScene("LeaderBoard");
                        rankingmanager.fromscene = 1;
                    }
                    break;
                case 4:
                    SceneManager.LoadScene("tweet", LoadSceneMode.Additive);
                    break;
            }
            count = 0;
        }
    }
}
