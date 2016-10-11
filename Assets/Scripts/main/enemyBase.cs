using UnityEngine;
using System.Collections;

public class enemyBase : MonoBehaviour {


    public float speed = 7;            //移動速度
 
    public float nextautomovetime = 1;   //次にオート移動するまでの時間
    public float automovecount;        //オート移動カウント
    public float autodir;                //オート移動の方向
    public bool findPlayer = false;    //プレイヤーを見つけたかどうか
    public float widehalf_range = 5;         //移動範囲(右・左半分）
    public Vector2 wide_rangemiddle;    //移動範囲の中心点

    //攻撃関連
    public bool attack = false;           //攻撃するかどうか
    public GameObject bullet;             //攻撃弾のオブジェクト
    public float attack_timing=1;           //次に攻撃するまでの時間
    private float bulletradi;
    private bool attackfinish_flag = false;

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

    public void set_bulletradi(float nextradi)
    {
        bulletradi = nextradi;
    }

    //攻撃
    IEnumerator Sample()
    {

        attackfinish_flag = true;
        while (findPlayer)
        {
            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(0, 0, bulletradi * Mathf.Rad2Deg - 90);

            // 弾をプレイヤーと同じ位置/角度で作成
            Instantiate(bullet, transform.position, rotation);
            // 0.05秒待つ
            yield return new WaitForSeconds(attack_timing);
        }
        attackfinish_flag = false;

    }

    public bool get_attackfinish_flag()
    {
        return attackfinish_flag;
    }
}
