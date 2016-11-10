using UnityEngine;
using System.Collections;

public class flyEnemy : enemyBase
{
    public float heighthalf_range = 5;    //移動範囲(上・下半分）


    private Animator anime;

    void Start()
    {
        //オート移動の中心点の初期化

        //横
        base.wide_rangemiddle.x = transform.position.x;
        base.wide_rangemiddle.y = transform.position.y;

        base.autodir = Random.Range(0, 361);
        base.autodir = base.autodir * Mathf.Deg2Rad;
        set_face(0);
        if (anime != null)
        {
            if (Mathf.Cos(autodir) > 0)
            {
                anime.SetTrigger("right");
                set_face(1);
            }
            else if (Mathf.Cos(autodir) < 0)
            {
                anime.SetTrigger("left");
                set_face(-1);
            }
            else
            {
                anime.SetTrigger("normal");
                set_face(0);
            }
        }

        set_spRenderer(GetComponent<SpriteRenderer>());

        anime = GetComponent<Animator>();
        if(anime==null)
            set_face(0);
    }

    // Update is called once per frame
    void Update()
    {
        disappear_Chara();
        automovecount += Time.deltaTime;
        if (get_findPlayer() == false && !get_disappear_flag())
            autoMove();
        if(anime==null)
            set_face(0);
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

            //元の初期位置との距離差
            Vector2 position_sa;
            position_sa.x = wide_rangemiddle.x - this.transform.position.x;
            position_sa.y = wide_rangemiddle.y - this.transform.position.y;

            float radi = Mathf.Atan2(position_sa.y, position_sa.x);
            transform.Translate(Mathf.Cos(radi) * Time.deltaTime * base.speed, Mathf.Sin(radi) * Time.deltaTime * base.speed, 0);

           
            if (anime != null)
            {
                if (Mathf.Cos(radi) > 0)
                {
                    anime.SetTrigger("right");
                    set_face(1);
                }
                else if (Mathf.Cos(radi) < 0)
                {
                    anime.SetTrigger("left");
                    set_face(-1);
                }
                else
                {
                    anime.SetTrigger("normal");
                    set_face(0);
                }
            }
        }

        //オート移動範囲内
        else
        {

            float refix = 0.1f;
            if (base.automovecount > nextautomovetime)
            {
                base.autodir = Random.Range(0, 361);
                base.autodir = base.autodir * Mathf.Deg2Rad;
                base.automovecount = 0;
            }
            p = new Vector3(p.x + Mathf.Cos(autodir) * this.speed * Time.deltaTime, p.y + Mathf.Sin(autodir) * this.speed * Time.deltaTime, p.z);
            p.x = (p.x > base.wide_rangemiddle.x + widehalf_range - refix) ? base.wide_rangemiddle.x + widehalf_range - refix : (p.x < base.wide_rangemiddle.x - widehalf_range + refix) ? base.wide_rangemiddle.x - widehalf_range + refix : p.x;
            p.y = (p.y > base.wide_rangemiddle.y + heighthalf_range - refix) ? base.wide_rangemiddle.y + heighthalf_range - refix : (p.y < wide_rangemiddle.y - heighthalf_range + refix) ? base.wide_rangemiddle.y - heighthalf_range + refix : p.y;
            this.transform.position = p;

            if (anime != null)
            {
                if (p.x >= base.wide_rangemiddle.x + widehalf_range - refix || p.x <= base.wide_rangemiddle.x - widehalf_range + refix)
                {
                    this.anime.SetTrigger("normal");
                    set_face(0);
                }
                else if (p.y >= wide_rangemiddle.y + heighthalf_range - refix || p.y <= wide_rangemiddle.y - heighthalf_range + refix)
                {
                    this.anime.SetTrigger("normal");
                    set_face(0);
                }

                else
                {
                    if (Mathf.Cos(autodir) > 0)
                    {
                        anime.SetTrigger("right");
                        set_face(1);
                    }
                    else if (Mathf.Cos(autodir) < 0)
                    {
                        anime.SetTrigger("left");
                        set_face(-1);
                    }
                    else
                    {
                        anime.SetTrigger("normal");
                        set_face(0);
                    }
                }
            }


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

        if (anime != null)
        {
            if (Mathf.Cos(radi) * Time.deltaTime * base.speed > 0)
            {
                anime.SetTrigger("right");
                set_face(1);
            }
            else if (Mathf.Cos(radi) * Time.deltaTime * base.speed < 0)
            {
                anime.SetTrigger("left");
                set_face(-1);
            }
            else
            {
                anime.SetTrigger("normal");
                set_face(0);
            }

        }

        base.set_bulletradi(radi);
        if (attack && !get_attackfinish_flag())
            StartCoroutine("attackFunc", radi);

    }
}
