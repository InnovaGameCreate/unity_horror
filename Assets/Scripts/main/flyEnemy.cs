using UnityEngine;
using System.Collections;

public class flyEnemy : enemyBase
{
    public float heighthalf_range = 5;    //移動範囲(上・下半分）
    public Vector2 height_rangemiddle;    //縦移動範囲の中心点
    

    void Start()
    {
        //オート移動の中心点の初期化
        //縦
        height_rangemiddle.x = transform.position.x;
        height_rangemiddle.y = transform.position.y;
        //横
        base.wide_rangemiddle.x = transform.position.x;
        base.wide_rangemiddle.y = transform.position.y;

        base.autodir = Random.Range(-1, 2);

    

    }

    // Update is called once per frame
    void Update()
    {
        automovecount += Time.deltaTime;
        if (findPlayer == false)
            autoMove();
   
    }

    public override void autoMove()
    {
        
        Vector3 p = this.transform.position;
        //オート移動範囲外なら初期座標に戻る
        if (p.x > base.wide_rangemiddle.x + widehalf_range ||
            p.x < base.wide_rangemiddle.x - widehalf_range ||
            p.y > base.wide_rangemiddle.y + heighthalf_range ||
            p.y < base.wide_rangemiddle.y - heighthalf_range)
        {
            //プレイヤーとの距離差
            Vector2 position_sa;
            position_sa.x = height_rangemiddle.x - this.transform.position.x;
            position_sa.y = height_rangemiddle.y - this.transform.position.y;

            float radi = Mathf.Atan2(position_sa.y, position_sa.x);
            transform.Translate(Mathf.Cos(radi) * Time.deltaTime * base.speed, Mathf.Sin(radi) * Time.deltaTime * base.speed, 0);

        }

        //オート移動範囲内
        else
        {
            if (base.automovecount > nextautomovetime)
            {
                base.autodir = Random.Range(0, 361);
                base.autodir = base.autodir * Mathf.Deg2Rad;
                base.automovecount = 0;
            }
            p = new Vector3(p.x + Mathf.Cos(autodir) * this.speed * Time.deltaTime, p.y + Mathf.Sin(autodir) * this.speed * Time.deltaTime, p.z);

            p.x = (p.x > base.wide_rangemiddle.x + widehalf_range) ? base.wide_rangemiddle.x + widehalf_range : (p.x < base.wide_rangemiddle.x - widehalf_range) ? base.wide_rangemiddle.x - widehalf_range : p.x;
            p.y = (p.y > base.wide_rangemiddle.y + heighthalf_range) ? base.wide_rangemiddle.y + heighthalf_range : (p.y < height_rangemiddle.y - heighthalf_range) ? base.wide_rangemiddle.y - heighthalf_range : p.y;

            this.transform.position = p;
        }
    }

    public override void chasePlayer(Collider player)
    {
        //プレイヤーとの距離差
        Vector2 position_sa;
        position_sa.x = player.transform.position.x - this.transform.position.x;
        position_sa.y = player.transform.position.y - this.transform.position.y;
  
        float radi = Mathf.Atan2(position_sa.y, position_sa.x);
        transform.Translate(Mathf.Cos(radi) * Time.deltaTime * base.speed, Mathf.Sin(radi) * Time.deltaTime * base.speed, 0);
        base.set_bulletradi(radi);
        if (attack && !get_attackfinish_flag())          
            StartCoroutine("Sample", radi);
        
    }
}
