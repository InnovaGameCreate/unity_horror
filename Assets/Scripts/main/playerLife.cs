using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerLife : MonoBehaviour
{

    static private int lifevalue = 3;
    private float count;
    static private bool flag;
    private bool deadflag;
    private GameObject hero;
    public GameObject backfont;
    public static string[] scenename = {
         "チュートリアル",
              "ステージ1",
              "ステージ2",
              "ステージ3",
              "ステージ4",
              "ステージ5"

        };
    //どのタイミングでLIFEを初期化するか
    public void set_flag(bool num)
    {
        flag = num;
    }
    // Use this for initialization
    void Start()
    {
        hero = GameObject.Find("プレイヤー");
        if (flag == false)
            lifevalue = 3;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text =backfont.GetComponent<Text>().text= lifevalue.ToString();
        if (deadflag == true)
        {
            count += Time.deltaTime;
            hero.GetComponent<heromove>().set_exlock();
            if (count > 2)
            {
                for (int i = 0; i < scenename.Length; i++)
                {
                    if (lifevalue == 1)
                    {
                        flag = false;
                        SceneManager.LoadScene("gameover");           //ゲームオーバーシーンへ
                    }
                    else if (SceneManager.GetSceneByName(scenename[i]).isLoaded == true)
                        SceneManager.LoadScene(scenename[i]); //リスタート

                }
                count = 0;
                deadflag = false;
                lifevalue--;
            }
        }
    }


    //ライフの減少とそれに伴うシーン処理
    public bool minus_life()
    {
        
        if (lifevalue > 0)
        {
            flag = true;
            deadflag = true;
            hero.GetComponent<Animator>().SetTrigger("dead");
            hero.GetComponent<heromove>().set_exlock();
  

        }
        else 
        {
            flag = false;
            SceneManager.LoadScene("gameover");           //ゲームオーバーシーンへ

        }
     
        Resources.UnloadUnusedAssets();

        return false;
    }
}
