using UnityEngine;
using System.Collections;

public class groundEnemy : enemyBase
{
    private Animator anime;
    void Start()
    {
        //オート移動の中心点の初期化
        base.wide_rangemiddle.x = transform.position.x;
        base.wide_rangemiddle.y = transform.position.y;

        base.autodir = Random.Range(-1, 2);
        set_face(base.autodir);
      
        set_spRenderer(GetComponent<SpriteRenderer>());

        anime = GetComponent<Animator>();

        if(anime==null)
            set_face(0);


        if (anime != null)
            switch ((int)base.autodir)
            {
                case 0:
                    this.anime.SetTrigger("normal");
                    break;
                case -1:
                    this.anime.SetTrigger("left");
                    break;
                case 1:
                    this.anime.SetTrigger("right");
                    break;
            }
    }


    void Update()
    {
        disappear_Chara();
        automovecount += Time.deltaTime;

  
        if (get_findPlayer() == false && !get_disappear_flag())
            autoMove();

    }

    public override void autoMove()
    {
        Vector3 p = this.transform.position;
        //オート移動範囲外なら初期座標に戻る
        //左
        if (p.x > base.wide_rangemiddle.x + widehalf_range)
        {
            transform.Translate(-1 * this.speed * Time.deltaTime, 0, 0);
            if (anime != null)
            {
                this.anime.SetTrigger("left");
                set_face(-1);
            }
        }
        //右
        else if (p.x < base.wide_rangemiddle.x - widehalf_range)
        {
            transform.Translate(1 * this.speed * Time.deltaTime, 0, 0);
            if (anime != null)
            {
                this.anime.SetTrigger("right");
                set_face(1);
            }
        }
        //オート移動範囲内
        else
        {
            if (base.automovecount > nextautomovetime)
            {
                base.autodir = Random.Range(-1, 2);
                base.automovecount = 0;
                if(anime!=null)
                set_face(base.autodir);
   
            }

            p = new Vector3(p.x + base.autodir * this.speed * Time.deltaTime, p.y, p.z);

            p.x = (p.x > base.wide_rangemiddle.x + widehalf_range) ? base.wide_rangemiddle.x + widehalf_range : (p.x < base.wide_rangemiddle.x - widehalf_range) ? base.wide_rangemiddle.x - widehalf_range : p.x;

            if (anime != null)
            {
                if (p.x == base.wide_rangemiddle.x + widehalf_range || p.x == base.wide_rangemiddle.x - widehalf_range)
                {
                    this.anime.SetTrigger("normal");
                    set_face(0);

                }
                else
                    switch ((int)base.autodir)
                    {
                        case 0:
                            this.anime.SetTrigger("normal");
                            break;
                        case -1:
                            this.anime.SetTrigger("left");
                            break;
                        case 1:
                            this.anime.SetTrigger("right");
                            break;
                    }
            }else
                set_face(0);

            this.transform.position = p;


        }
    }

    public override void chasePlayer(Collider player)
    {
        //ユニティちゃんに向かって敵が移動
        Vector3 p = this.transform.position;
        int dir = (player.transform.position.x > this.transform.position.x + 0.3f) ? 1 : (player.transform.position.x < this.transform.position.x - 0.3f) ? -1 : 0;
        p = this.transform.position = new Vector3(p.x + dir * this.speed * Time.deltaTime * 0.9f, p.y, p.z);

        set_face(dir);
        if (anime != null)
            switch ((int)dir)
            {
                case -1:
                    this.anime.SetTrigger("left");
                    break;
                case 1:
                    this.anime.SetTrigger("right");
                    break;
            }

        this.transform.position = p;

        if (attack)
        {
            //プレイヤーとの距離差
            Vector2 position_sa;
            position_sa.x = player.transform.position.x - this.transform.position.x;
            position_sa.y = player.transform.position.y - this.transform.position.y;

            float radi = Mathf.Atan2(position_sa.y, position_sa.x);
            base.set_bulletradi(radi);
            if (!get_attackfinish_flag())
                StartCoroutine("attackFunc", radi);
        }
    }

}