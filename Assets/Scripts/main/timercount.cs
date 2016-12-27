using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class timercount : MonoBehaviour {
    public int min=3;
    public int sec;
    public static float usedtime;
    private float count;
	// Use this for initialization
	void Start () {
        count = min * 60 + sec;
        usedtime = 0;

    }
	
	// Update is called once per frame
	void Update () {
        count -= Time.deltaTime;
        usedtime += Time.deltaTime;
        GetComponent<Text>().text = ((int)(count / 60)).ToString() +"分"+((int)count%60).ToString()+"秒";

    }
}
