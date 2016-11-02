using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class stage_select : MonoBehaviour
{

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

    private Dir nowdir=Dir.none;
    private Stage stage_is=0;
    Transform []stage;
    // Use this for initialization
    void Start()
    {
        stage= new Transform[(int)Stage.None];
        // 子要素を全て取得する
        for (int i = 0; i < transform.childCount; i++)
        {

            stage[i] = transform.GetChild(i);
        }
        stage[(int)stage_is].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (nowdir)
        {
            case Dir.none:
               
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
                if (stage_is<Stage.None-1&&Input.GetKeyDown(KeyCode.RightArrow))
                {
                    nowdir = Dir.righting;
                    stage_is++;
                    break;
                }
                else if (stage_is > Stage.tutorial && Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    nowdir = Dir.lefting;
                    stage_is--;
                    break;
                }

                break;
            case Dir.righting:
            case Dir.lefting:
                for (int i = 0; i < transform.childCount; i++)
                {
                    stage[i].gameObject.SetActive(false);
                }

                stage[(int)stage_is].gameObject.SetActive(true);
                nowdir = Dir.none;
                    break;


        }

    }

}