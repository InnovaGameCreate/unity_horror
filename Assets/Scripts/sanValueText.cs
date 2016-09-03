using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class sanValueText : MonoBehaviour
{
    public int san;     //san値
   
    public int san_addspeed = 4;   //san値増加抑制量
    public float san_minusspeed = 0.3f;   //san値減少時間間隔
    private int sanmax = 100;  //san最大値
    private float timeElapsed;
    // Use this for initialization
    void Start()
    {
        sanmax *= san_addspeed;

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = "SAN値：" + (san / san_addspeed).ToString();

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= san_minusspeed)
        {

            san = san > 0 ? san - 1 : 0;
            timeElapsed = 0.0f;
        }

        if (san > sanmax)
            san = sanmax;


    }
}
