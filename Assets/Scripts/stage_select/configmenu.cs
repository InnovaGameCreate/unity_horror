using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class configmenu : MonoBehaviour {
    private int selecting = 0;
    private int maxselect = 3;
    public Text rankingtext;
    AudioSource[] sound = new AudioSource[2];
    private float count;
    // Use this for initialization
    void Start()
    {
        if (stage_select.stage_is == stage_select.Stage.tutorial)
            rankingtext.color = Color.gray;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        for (int i = 0; i < 2; i++)
            sound[i] = audioSources[i];
    }

    // Update is called once per frame
    void Update()
    {
        //escメニューの選択
        if (selecting < maxselect - 1 && Input.GetKeyDown(KeyCode.DownArrow))
        {
            sound[0].Play();
            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
            pos.y -= 20;
            GetComponent<RectTransform>().anchoredPosition = pos;
            selecting++;
        }
        else if (selecting > 0 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            sound[0].Play();
            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
            pos.y += 20;
            GetComponent<RectTransform>().anchoredPosition = pos;
            selecting--;
        }
        if (count==0&&Input.GetKeyDown(KeyCode.Return))
        {
            count++;
            sound[1].Play();
        }
        if (count > 0)
            count += Time.deltaTime;
        if (count > 1.1f)
        {
            switch (selecting)
            {
                case 0:
                    SceneManager.UnloadScene("configmenu");
                    Resources.UnloadUnusedAssets();
                    break;
                case 1:


                    if (stage_select.stage_is == stage_select.Stage.tutorial)
                    {

                    }
                    else if (FindObjectOfType<UserAuth>().currentPlayer() == "")
                    {

                    }
                    else
                    {
                        SceneManager.LoadScene("LeaderBoard");
                        rankingmanager.fromscene = 0;
                    }
                    break;
                case 2:
                    Application.Quit();
                    break;
            }
        }

    }
    //どれを選択してるか
    public int get_selecting()
    {
        return selecting;
    }
}
