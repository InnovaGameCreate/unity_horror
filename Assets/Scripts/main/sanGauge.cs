using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sanGauge : MonoBehaviour {
    public sanValueText sanText; //外部のsanValueTexオブジェクトを見えるよう定義

                                 // Use this for initialization
    void Start () {
	
	}
	

	//sanゲージの割合計算
	void Update () {
        GetComponent<Slider>().value = (float)sanText.get_san() / sanText.get_sanmax();
    }
}
