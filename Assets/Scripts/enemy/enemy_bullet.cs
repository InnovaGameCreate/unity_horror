using UnityEngine;
using System.Collections;

public class enemy_bullet : MonoBehaviour {

    public int speed =10;        //速さ
    public int atk=1;             //SAN値減少量
    public bool chasePlayer=false;    //プレイヤーを常時
    public float timeOut=7;             //何秒後に弾が自動的に消えるか
    
    private GameObject player;      //狙っているプレイヤー

    private float timeElapsed;

    public void set_player(GameObject target)
    {
        player = target;
    }

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.up.normalized * speed;
        StartCoroutine("attackFunc");
    }

    // Update is called once per frame
    void Update () {
       

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeOut)
        {
            timeElapsed = 0.0f;
            Destroy(this.gameObject);
        }
      
	}

    //角度調整　追尾
    IEnumerator attackFunc()
    {
        while (true)
        {
            if (chasePlayer == true && player != null)
            {
                //プレイヤーとの距離差
                Vector2 position_sa;
                position_sa.x = player.transform.position.x - this.transform.position.x;
                position_sa.y = player.transform.position.y - this.transform.position.y;

                float radi = Mathf.Atan2(position_sa.y, position_sa.x);

                Quaternion rotation = Quaternion.identity;
                rotation.eulerAngles = new Vector3(0, 0, radi * Mathf.Rad2Deg - 90);
           
                transform.rotation = rotation;

                GetComponent<Rigidbody>().velocity = transform.up.normalized * speed;
            }
            if (player.GetComponent<heromove>().get_state() == heromove.State.Invincible ||
                player.GetComponent<heromove>().get_state() == heromove.State.InvincibleMove)
                break;
            yield return new WaitForSeconds(1);
        }

    }

    //オブジェクトが衝突したとき
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<heromove>().attacked(atk);
        }
        Destroy(this.gameObject);
    }

}
