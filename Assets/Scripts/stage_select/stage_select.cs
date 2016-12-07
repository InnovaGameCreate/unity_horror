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
    static private Stage stage_is = 0;
    static Vector2 charaposi;

    private bool moving;

    public bool []clearflag=new bool[(int)Stage.None];

    private Transform[] stage;


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


        if(charaposi.x!=0)
    stage[6].GetComponent<RectTransform>().anchoredPosition = charaposi ;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            SaveData.Clear();
   
            arrow[0].SetActive(false);
        arrow[1].SetActive(false);
        arrow[2].SetActive(false);


        if (Input.GetKeyDown(KeyCode.Return))
        {
            charaposi=stage[6].GetComponent<RectTransform>().anchoredPosition;
            SceneManager.LoadScene(playerLife.scenename[(int)stage_is]);
      
    
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
        {

            moving = false;
        }
        //Vector2 posi= stage[(int)stage_is].GetComponent<RectTransform>().anchoredPosition;
        //posi.x -= 15;
        //posi.y += 10;

        //stage[6].GetComponent<RectTransform>().anchoredPosition = posi;


        if (!moving)
        {
            stage[6].GetComponent<Animator>().SetInteger("New Int", 0);
            switch (stage_is)
            {
                case Stage.tutorial:
                    arrow[0].SetActive(true);
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        //アニメーション
                        stage[6].GetComponent<Animator>().SetInteger("New Int", 1);
                        stage_is++;
                    }
                    break;
                case Stage.stage1:


                    arrow[1].SetActive(true);

                    if (SaveData.GetInt("stage1") == 0)
                        break;
                    arrow[0].SetActive(true);

         
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        stage[6].GetComponent<Animator>().SetInteger("New Int", 1);
                        stage_is++;
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        stage[6].GetComponent<Animator>().SetInteger("New Int", -1);
                        stage_is--;
                    }
                    break;
                case Stage.stage2:
   
           
                    arrow[1].SetActive(true);
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        stage[6].GetComponent<Animator>().SetInteger("New Int", -1);
                        stage_is--;
                    
                    }

                    if (SaveData.GetInt("stage2") == 0)
                        break;
                    arrow[0].SetActive(true);
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        stage[6].GetComponent<Animator>().SetInteger("New Int", 1);
                        stage_is++;
                    }
            
                    break;
                case Stage.stage3:
                    arrow[2].SetActive(true);
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        stage[6].GetComponent<Animator>().SetInteger("New Int", -1);
                        stage_is--;
                    }
                    if (SaveData.GetInt("stage3") == 0)
                        break;
                    arrow[1].SetActive(true);
                
          

                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        stage[6].GetComponent<Animator>().SetInteger("New Int", -1);
                        stage_is++;
                    }
                 
                    break;
                case Stage.stage4:
             

           
                    arrow[0].SetActive(true);
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        stage[6].GetComponent<Animator>().SetInteger("New Int", 1);
                        stage_is--;
                    }
                    if (SaveData.GetInt("stage4") == 0)
                        break;
                    arrow[1].SetActive(true);
                
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        stage[6].GetComponent<Animator>().SetInteger("New Int", -1);
                        stage_is++;
                    }
                    break;
                case Stage.stage5:
                    arrow[0].SetActive(true);

                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        stage[6].GetComponent<Animator>().SetInteger("New Int", 1);
                        stage_is--;
                    }
                    break;
            }
            //     stage[6].GetCompmovingonent<RectTransform>().rotation = rota;
            moving = true;

        }
        }

}