using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class configmenu : MonoBehaviour {
    private int selecting = 0;
    private int maxselect = 3;
    public Text rankingtext;
    // Use this for initialization
    void Start()
    {
        if (stage_select.stage_is == stage_select.Stage.tutorial)
            rankingtext.color = Color.gray;

    }

    // Update is called once per frame
    void Update()
    {
        //escメニューの選択
        if (selecting < maxselect - 1 && Input.GetKeyDown(KeyCode.DownArrow))
        {

            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
            pos.y -= 20;
            GetComponent<RectTransform>().anchoredPosition = pos;
            selecting++;
        }
        else if (selecting > 0 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
            pos.y += 20;
            GetComponent<RectTransform>().anchoredPosition = pos;
            selecting--;
        }
        if (Input.GetKeyDown(KeyCode.Return))
            switch (selecting)
            {
                case 0:
                    SceneManager.UnloadScene("configmenu");
                    Resources.UnloadUnusedAssets();
                    break;
                case 1:
                

                    if (stage_select.stage_is == stage_select.Stage.tutorial)
                    {

                    }else if (FindObjectOfType<UserAuth>().currentPlayer() == "")
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
    //どれを選択してるか
    public int get_selecting()
    {
        return selecting;
    }
}
