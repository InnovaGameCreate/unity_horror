using UnityEngine;
using System.Collections;

public class enemyBase : MonoBehaviour {


    public float speed = 7;            //移動速度
    public float widehalf_range = 5;         //移動範囲(右・左半分）
    public float nextautomovetime = 1;   //次にオート移動するまでの時間

    public bool findPlayer = false;    //プレイヤーを見つけたかどうか
    public float automovecount;        //オート移動カウント
    public Vector2 wide_rangemiddle;    //移動範囲の中心点
    public float autodir;                //オート移動の方向


    public bool get_findPlayer()
    {
        return findPlayer;
    }

    public void set_findPlayer(bool next)
    {
        findPlayer = next;
    }

    //自由移動
    public virtual void autoMove() { }

    //プレイヤーへの追跡　
    public virtual void chasePlayer(Collider player) { }

}
