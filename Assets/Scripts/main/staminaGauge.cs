using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class staminaGauge : MonoBehaviour {
    private float stamina = 100;     //スタミナ
    private float staminamax = 100;  //スタミナ最大値

    public float stamina_plustime = 0.5f;   //スタミナ増加時間間隔


    public bool debugmode;   //スタミナが減らない状態にするかどうか
    private float timeElapsed;

    private float staminaautocount = 0;
    public float sanautocountmax = 3;     //走ってから自然回復するまでの時間


    // Use this for initialization
    void Start () {

        stamina = staminamax;
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Slider>().value = (float)get_stamina() / get_staminamax();

        //スタミナの自然回復
        if (staminaautocount > 0)
        {
            staminaautocount -= Time.deltaTime;
        }
        else
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= stamina_plustime)
            {

                stamina = stamina < staminamax ? stamina + 1 : staminamax;
                timeElapsed = 0.0f;

            }
        }
    }

    //スタミナの減少
    public void minus_stamina(float minus)
    {
        if (debugmode)
            return;

        staminaautocount = sanautocountmax;
        stamina = stamina > 0 ? stamina - minus : 0;
    }


    public float get_stamina()
    {
        return stamina;
    }

    public float get_staminamax()
    {
        return staminamax;
    }
}
