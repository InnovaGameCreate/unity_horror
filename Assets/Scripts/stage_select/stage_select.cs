using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class stage_select : MonoBehaviour
{

    //ステージ
    private enum Stage
    {
        tutorial,
        stage1,
        stage2,
        stage3,
        stage4,
        stage5,
        None
    }
    //レベル
    private enum Level
    {
        easy,
        normal,
        hard,
        None
    }

    private Stage stage_is;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("main");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
            pos.y -= 20;
            GetComponent<RectTransform>().anchoredPosition = pos;
            stage_is++;

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
            pos.y += 20;
            GetComponent<RectTransform>().anchoredPosition = pos;
            stage_is--;
        }
    }

}