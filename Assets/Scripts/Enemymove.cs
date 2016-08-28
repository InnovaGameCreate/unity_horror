using UnityEngine;
using System.Collections;

public class Enemymove : MonoBehaviour {

    public float speed=0.2f;
    public bool findPlayer = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void chasePlayer(Collider player)
    {
        //ユニティちゃんに向かって敵が移動
        Vector3 p = this.transform.position;
        int dir= (player.transform.position.x > this.transform.position.x)?1: (player.transform.position.x < this.transform.position.x)? - 1:0;
        p = this.transform.position = new Vector3(p.x + dir*this.speed, p.y, p.z);

        this.transform.position = p;
    }


}
