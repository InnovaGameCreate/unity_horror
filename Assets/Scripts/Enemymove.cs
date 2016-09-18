using UnityEngine;
using System.Collections;

public class Enemymove : MonoBehaviour {

    public float speed=7;            //移動速度
    public float halfrange = 5;         //移動範囲(右・左半分）
    public float nextautomovetime = 1;   //次にオート移動するまでの時間

    private bool findPlayer = false;    //プレイヤーを見つけたかどうか
    private float automovecount;        //オート移動カウント
    private Vector2 rangemiddle;        //移動範囲の中心点
    private int autodir;                //オート移動の方向


    public bool get_findPlayer()
    {
        return findPlayer;
    }

    public void set_findPlayer(bool next)
    {
        findPlayer = next;
    }

	// Use this for initialization
	void Start () {
        //オート移動の中心点の初期化
        rangemiddle.x = transform.position.x;
        rangemiddle.y = transform.position.y;

        autodir = Random.Range(-1, 2);
    }
	
	// Update is called once per frame
	void Update () {
        automovecount += Time.deltaTime;
        if(findPlayer==false)
        autoMove();

    }

    //ランダム移動
private void autoMove()
    {
        Vector3 p = this.transform.position;
        //オート移動範囲外なら初期座標に戻る
        if (p.x > rangemiddle.x + halfrange)
            transform.Translate(-1 * this.speed * Time.deltaTime, 0, 0);
        else if (p.x < rangemiddle.x - halfrange)
            transform.Translate(1 * this.speed * Time.deltaTime, 0, 0);
        //オート移動範囲内
        else 
        {
            if (automovecount > nextautomovetime)
            {
                autodir = Random.Range(-1, 2);
                automovecount = 0;
            }
            p = new Vector3(p.x + autodir * this.speed * Time.deltaTime, p.y, p.z);
    
            p.x = (p.x > rangemiddle.x + halfrange) ? rangemiddle.x + halfrange : (p.x < rangemiddle.x - halfrange) ? rangemiddle.x - halfrange : p.x;
            this.transform.position = p;
        }

    }


    public void chasePlayer(Collider player)
    {
        //ユニティちゃんに向かって敵が移動
        Vector3 p = this.transform.position;
        int dir= (player.transform.position.x > this.transform.position.x+0.3f)?1: (player.transform.position.x < this.transform.position.x - 0.3f) ? - 1:0;
        p = this.transform.position = new Vector3(p.x + dir*this.speed * Time.deltaTime*0.9f, p.y, p.z);

        this.transform.position = p;
    }


}
