using UnityEngine;
using System.Collections;

public class Enemymove : MonoBehaviour {

    public float speed=0.2f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void chasePlayer(Collider player)
    {
        //ユニティちゃんを移動
        Vector3 p = this.transform.position;
        int dir= (player.transform.position.x > this.transform.position.x)?1: (player.transform.position.x < this.transform.position.x)? - 1:0;
        p = this.transform.position = new Vector3(p.x + dir*this.speed, p.y, p.z);
        p.z = p.z > 5 ? 5 : (p.z < 0 ? 0 : p.z);    //Z=0以上かつ5未満の範囲でZ軸移動できる

        //カメラの位置を設定
        this.transform.position = p;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") )
            chasePlayer(other);


    }
}
