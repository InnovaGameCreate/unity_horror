using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class sanValueText : MonoBehaviour
{
private int san=100;     //san値
    private int sanmax = 100;  //san最大値

    public int san_minusspeed = 4;   //san値減少抑制量
    public float san_plustime = 0.3f;   //san値増加時間間隔

    public playerLife life_info;

    private float timeElapsed;

 
    // Use this for initialization
    void Start()
    {
        sanmax *= san_minusspeed;
        san = sanmax;
    }

    // Update is called once per frame
    void Update()
    {


        timeElapsed += Time.deltaTime;
        this.GetComponent<Text>().text = "SAN値：" + (san / san_minusspeed).ToString();
        if (timeElapsed >= san_plustime)
        {
         
            san = san < sanmax ? san + 1 : sanmax;
            timeElapsed = 0.0f;
          
        }
        
       
    }
   
    //san値の減少
    public void minus_san()
    {

        san = san > 0 ? san - 1:0 ;
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


    public int get_san()
    {
        return san;
    }

    public int get_sanmax()
    {
        return sanmax;
    }

}
