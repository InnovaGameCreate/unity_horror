using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class stage_select : MonoBehaviour
{

    public GameObject[] arrow;
    //ステージ
    //以下の順に入れ子構造にすること
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

    private enum Dir
    {
        righting,
        lefting,
        none
    }

    private Dir nowdir = Dir.none;
    private Stage stage_is = 0;
    private bool moving;

    Transform[] stage;
    // Use this for initialization
    void Start()
    {
        stage = new Transform[(int)Stage.None + 1];
        // 子要素を全て取得する
        for (int i = 0; i < transform.childCount; i++)
        {

            stage[i] = transform.GetChild(i);
        }
        // stage[(int)stage_is].gameObject.SetActive(true);
        Vector2 posi = stage[(int)stage_is].GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (stage_is)
            {
                case Stage.tutorial:
                    break;
                case Stage.stage1:
                    break;
                case Stage.stage2:
                    break;
                case Stage.stage3:
                    break;
                case Stage.stage4:
                    break;
                case Stage.stage5:
                    break;
            }
            SceneManager.LoadScene("main");
        }
        Vector2 samp = stage[(int)stage_is].GetComponent<RectTransform>().anchoredPosition;
        samp.y += 20;
        if (stage[6].GetComponent<RectTransform>().anchoredPosition.x < samp.x - 15 ||
            stage[6].GetComponent<RectTransform>().anchoredPosition.x > samp.x + 15 ||
                   stage[6].GetComponent<RectTransform>().anchoredPosition.y < samp.y - 15 ||
                                stage[6].GetComponent<RectTransform>().anchoredPosition.y > samp.y + 15)
        {
            Vector2 point_sa;
            point_sa.x = samp.x - stage[6].GetComponent<RectTransform>().anchoredPosition.x;
            point_sa.y = samp.y - stage[6].GetComponent<RectTransform>().anchoredPosition.y;

            float radi = Mathf.Atan2(point_sa.y, point_sa.x);
       
            Vector2 posi = stage[6].GetComponent<RectTransform>().anchoredPosition;
            posi.x += Mathf.Cos(radi) * Time.deltaTime * 100;
            posi.y += Mathf.Sin(radi) * Time.deltaTime * 100;

            stage[6].GetComponent<RectTransform>().anchoredPosition = posi;
            moving = true;
        }
        else
            moving = false;
        //Vector2 posi= stage[(int)stage_is].GetComponent<RectTransform>().anchoredPosition;
        //posi.x -= 15;
        //posi.y += 10;

        //stage[6].GetComponent<RectTransform>().anchoredPosition = posi;
        arrow[0].SetActive(false);
        arrow[1].SetActive(false);
        arrow[2].SetActive(false);
        if (!moving)
        switch (stage_is)
        {
            case Stage.tutorial:
                    arrow[0].SetActive(true);
                if (Input.GetKeyDown(KeyCode.RightArrow))
                    stage_is++;
                break;
            case Stage.stage1:
                    arrow[0].SetActive(true);
                    arrow[1].SetActive(true);
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    stage_is++;
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    stage_is--;
                break;
            case Stage.stage2:
                    arrow[0].SetActive(true);
                    arrow[1].SetActive(true);

                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    stage_is++;
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    stage_is--;
                break;
            case Stage.stage3:
                    arrow[1].SetActive(true);
                    arrow[2].SetActive(true);
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    stage_is++;
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                    stage_is--;

                break;
            case Stage.stage4:
                    arrow[0].SetActive(true);
                    arrow[1].SetActive(true);
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    stage_is--;
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    stage_is++;
                break;
            case Stage.stage5:
                    arrow[0].SetActive(true);
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    stage_is--;
                break;
        }


    }

}