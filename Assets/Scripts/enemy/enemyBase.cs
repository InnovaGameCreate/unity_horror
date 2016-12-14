using UnityEngine;
using System.Collections;

public class enemyBase : MonoBehaviour {
    private heromove state_info;

    public float normalspeed = 7;            //追跡中移動速度
    public float runspeed = 10;            //追跡中移動速度
   

    public float nextautomovetime = 1;   //次にオート移動するまでの時間
    public float automovecount;        //オート移動カウント
    public float autodir;                //オート移動の方向
    public bool chaseplayer = true;     //プレイヤーを追跡する（見つける）かどうか
    private bool findPlayer = false;    //プレイヤーを見つけたかどうか
    public float widehalf_range = 5;         //移動範囲(右・左半分）
    public Vector2 wide_rangemiddle;    //移動範囲の中心点
    public float disappear_time=0;      //（あたり判定が）消えてる時間 ※0なら消えるモード無効
    public float appear_time=0;      //（あたり判定が）現れてる時間 ※disappear_timeが有効なときのみ有効 ※0なら消えるモード無効
    public bool lockplayer=false;             //プレイヤーに当たった時　プレイヤーを一定時間固定させるかどうか
    //攻撃関連
    public bool attack = false;           //攻撃するかどうか
    public GameObject bullet;             //攻撃弾のオブジェクト
    public float attack_timing=1;           //次に攻撃するまでの時間
    private float bulletradi;
    private bool attackfinish_flag = false;

    private float disappear_count;
    private bool disappear_flag;
    private SpriteRenderer spRenderer;

    private GameObject targetplayer;        //狙っているプレイヤーのオブジェクト

    private float face = 1;                //敵の向いてる方向 
    public LayerMask mask;              //レイキャスト用マスク  groundとplayerを指定すること
    public int raynum;            //視認レーザーの数 
    public float rayangle;        //水平方向を中心とした視認範囲の角度
    public float raylength;       //視認の長さ

    public void set_state_info(heromove set)
    {
        state_info = set;
    }
    public heromove get_state_info()
    {
        return state_info;
    }

    public float get_face()
    {
        return face;
    }
    public void set_face(float next)
    {
        face=next;
    }

    //プレイヤーセットゲット
    public void set_targetplayer(GameObject taplayer)
    {
        targetplayer = taplayer;
    }
    public GameObject get_targetplayer()
    {
        return targetplayer;
    }


    public bool get_findPlayer()
    {
        return findPlayer;
    }

    public void set_findPlayer(bool next)
    {
        if (state_info != null)
        {
            if (get_findPlayer()==true&&next == false)
                state_info.lookedImg.GetComponent<lookedenemy>().set_lookedbyenemyadd(-1);
            if (get_findPlayer() == false && next == true)
                state_info.lookedImg.GetComponent<lookedenemy>().set_lookedbyenemyadd(1);
        }

        findPlayer = next;
    }

    //自由移動
    public virtual void autoMove() { }

    //プレイヤーへの追跡　
    public virtual void chasePlayer(Collider player) { }


    public bool get_disappear_flag()
    {
        return disappear_flag;
    }
    public void set_spRenderer(SpriteRenderer obj)
    {
        spRenderer = obj;
    }
    //半透明になってあたり判定が消える
    public void disappear_Chara()
    {
        //敵がプレイヤーを見つけたときは消えない
        if (disappear_time == 0||appear_time==0||findPlayer)
            return;
        disappear_count += Time.deltaTime;

        if (!disappear_flag && appear_time < disappear_count)
        {
            disappear_flag = true;
            GetComponent<BoxCollider>().enabled = false;
            var color = spRenderer.color;
            color.a = 0.5f;
            spRenderer.color = color;
            disappear_count = 0;
        

        }
        else if(disappear_flag&& disappear_time < disappear_count)
        {
            disappear_flag = false;
            GetComponent<BoxCollider>().enabled =true;
            var color = spRenderer.color;
            color.a = 1;
            spRenderer.color = color;
            disappear_count = 0;
        }
    }
    public void set_bulletradi(float nextradi)
    {
        bulletradi = nextradi;
    }

    //攻撃
    IEnumerator attackFunc()
    {
        attackfinish_flag = true;
        while (findPlayer)
        {
            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(0, 0, bulletradi * Mathf.Rad2Deg - 90);

            // 弾をプレイヤーと同じ位置/角度で作成
            GameObject newbullet = Instantiate(bullet, transform.position, rotation) as GameObject;
            newbullet.GetComponent<enemy_bullet>().set_player(get_targetplayer());
    
            yield return new WaitForSeconds(attack_timing);
        }
        attackfinish_flag = false;

    }

    public bool get_attackfinish_flag()
    {
        return attackfinish_flag;
    }
}
