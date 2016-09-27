using UnityEngine;
using System.Collections;

public class deleteObject : MonoBehaviour {

    public sanValueText sanText; //外部のsanValueTexオブジェクトを見えるよう定義
                   
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        //プレイヤーならライフ減少
        if (other.CompareTag("Player"))
            sanText.kill_san();
        //敵なら消滅
        if (other.CompareTag("Enemy"))
            Destroy(other.gameObject);

    }
}
