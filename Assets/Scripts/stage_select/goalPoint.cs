using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class goalPoint : MonoBehaviour
{
    private playerLife goal;     //lifevaluetextを指定すること
    private int stageno;
    private GameObject _child;
    private float count = 0;
    private GameObject player;
    private bool se;
    static public int newclear;
 
    // Use this for initialization
    void Start()
    {
        _child = transform.FindChild("Child").gameObject;
        stageno = heromove.nowstage;
        goal = GameObject.Find("LifeValueText").GetComponent<playerLife>();
    }

    // Update is called once per frame
    void Update()
    {
        if (count > 3)
        {

            goal.set_flag(false);
            if (SaveData.GetInt("stage" + stageno.ToString()) == 0)
            {
                SaveData.SetInt("stage" + stageno.ToString(), 1);
                //ステージ選択画面で外部ファイルからフラグを読み取るためここでセーブ
                SaveData.Save();
                newclear = stageno;
            }
            SceneManager.LoadScene("gameclear");
     
        }
        else if (count > 0)
        {
            if (se == false)
            {
                GetComponent<AudioSource>().Play();
                se = true;
            }
            count += Time.deltaTime;
            player.GetComponent<heromove>().set_exlock();
        }

    


    }

    void OnTriggerEnter(Collider other)
    {
        //敵と接触時
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            player.GetComponent<Animator>().SetTrigger("goal");
           count += Time.deltaTime;
            _child.SetActive(true);
        }

    }

}
