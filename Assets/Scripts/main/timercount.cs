using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class timercount : MonoBehaviour {
    public int min=3;
    public int sec;
    public static float usedtime;
    public GameObject shadow;
    public playerLife life;
    private float count;
    private bool se;
    // Use this for initialization
    void Start () {
        count = min * 60 + sec;
        usedtime = 0;

    }
	
	// Update is called once per frame
	void Update () {
        count -= Time.deltaTime;
        if (count < 0)
            count = 0;
        usedtime += Time.deltaTime;
        GetComponent<Text>().text =shadow.GetComponent<Text>().text= ((int)(count / 60)).ToString() +"分"+((int)count%60).ToString()+"秒";

        if (count <= 0)
        {
            life.minus_life();
            GetComponent<AudioSource>().Stop();
        }
        if (se == false&&count<=10)
        {
            GetComponent<Text>().color = Color.red;
            se = true;
            GetComponent<AudioSource>().Play();
        }
        if (count <= 10)
        {
            if ((int)count % 2 == 0)
                GetComponent<Text>().color = Color.red;
            else
                GetComponent<Text>().color = Color.white;
        }

    }
}
