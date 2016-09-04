using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class sanValueText : MonoBehaviour
{
    public int san=100;     //san値
    public int sanmax = 100;  //san最大値
    public int san_minusspeed = 4;   //san値減少抑制量
    public float san_plustime = 0.3f;   //san値増加時間間隔

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
        this.GetComponent<Text>().text = "SAN値：" + (san / san_minusspeed).ToString();

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= san_plustime)
        {

            san = san < sanmax ? san + 1 : sanmax;
            timeElapsed = 0.0f;
        }

        if (san < 0)
            san = 0;


    }
}
