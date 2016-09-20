using UnityEngine;
using System.Collections;

public class groundEnemy : enemyBase
{
    void Start()
    {
        //オート移動の中心点の初期化
        base.wide_rangemiddle.x = transform.position.x;
        base.wide_rangemiddle.y = transform.position.y;

        base.autodir = Random.Range(-1, 2);
    }


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
        if (p.x > base.wide_rangemiddle.x + widehalf_range)
            transform.Translate(-1 * this.speed * Time.deltaTime, 0, 0);
        else if (p.x < base.wide_rangemiddle.x - widehalf_range)
            transform.Translate(1 * this.speed * Time.deltaTime, 0, 0);
        //オート移動範囲内
        else
        {
            if (base.automovecount > nextautomovetime)
            {
                base.autodir = Random.Range(-1, 2);
                base.automovecount = 0;
            }
            p = new Vector3(p.x + base.autodir * this.speed * Time.deltaTime, p.y, p.z);

            p.x = (p.x > base.wide_rangemiddle.x + widehalf_range) ? base.wide_rangemiddle.x + widehalf_range : (p.x < base.wide_rangemiddle.x - widehalf_range) ? base.wide_rangemiddle.x - widehalf_range : p.x;
            this.transform.position = p;
        }
    }

    public override void chasePlayer(Collider player)
    {
        //ユニティちゃんに向かって敵が移動
        Vector3 p = this.transform.position;
        int dir = (player.transform.position.x > this.transform.position.x + 0.3f) ? 1 : (player.transform.position.x < this.transform.position.x - 0.3f) ? -1 : 0;
        p = this.transform.position = new Vector3(p.x + dir * this.speed * Time.deltaTime * 0.9f, p.y, p.z);

        this.transform.position = p;
    }

}