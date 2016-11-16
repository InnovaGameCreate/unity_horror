using UnityEngine;
using System.Collections;

public class enemydelete : MonoBehaviour {


    public GameObject expro;        //爆発エフェクトパーティクルを指定

    void OnTriggerStay(Collider other)
    {

        //敵なら消滅
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            Instantiate(expro, other.transform.position, Quaternion.identity);
        }

    }
}
