using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class sanValueText : MonoBehaviour
{
private float san=100;     //san値
    private float sanmax = 100;  //san最大値

    public int san_minusspeed = 4;   //san値減少抑制量
    public float san_plustime = 0.5f;   //san値増加時間間隔

    public playerLife life_info;

    private float timeElapsed;

    private float sanautocount=0;     
    public float sanautocountmax = 3;     //攻撃を受けてから自然回復するまでの時間


    // Use this for initialization
    void Start()
    {
       
        san = sanmax;
    }

    // Update is called once per frame
    void Update()
    {

        if (sanautocount > 0)
        {
            sanautocount -= Time.deltaTime;
        }
        else
        {
            timeElapsed += Time.deltaTime;
        
            if (timeElapsed >= san_plustime)
            {

                san = san < sanmax ? san + 1 : sanmax;
                timeElapsed = 0.0f;

            }
        }
        this.GetComponent<Text>().text = "SAN値：" + ((int)san).ToString();

    }
   
    //san値の減少
    public void minus_san(float minus)
    {
        sanautocount = sanautocountmax;
        san = san > 0 ? san - minus:0 ;
        if (san <= 0)
        {
            life_info.minus_life();
            san = sanmax;
        }

    }
    
    //即死
    public void kill_san()
    {
        san = 0;
        life_info.minus_life();
        san = sanmax;
    }


    public float get_san()
    {
        return san;
    }

    public float get_sanmax()
    {
        return sanmax;
    }

}
