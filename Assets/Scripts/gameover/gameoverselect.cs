using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameoverselect : MonoBehaviour {

    private int selecting = 0;
    private int maxselect = 2;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //escメニューの選択
        if (selecting < maxselect - 1 && Input.GetKeyDown(KeyCode.DownArrow))
        {

            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
            pos.y -= (34+49);
            GetComponent<RectTransform>().anchoredPosition = pos;
            selecting++;
        }
        else if (selecting > 0 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
            pos.y += (34 + 49);
            GetComponent<RectTransform>().anchoredPosition = pos;
            selecting--;
        }
        if (Input.GetKeyDown(KeyCode.Return))
            switch (selecting)
            {
                case 0:
                    SceneManager.LoadScene(playerLife.scenename[heromove.nowstage]);
                    break;
                case 1:
                    SceneManager.LoadScene("stage_select");
                    break;
            }


    }
}
