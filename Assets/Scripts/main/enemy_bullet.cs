using UnityEngine;
using System.Collections;

public class enemy_bullet : MonoBehaviour {

    public int speed =10;        //速さ
    public int atk=1;             //SAN値減少量
    private int count;
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.up.normalized * speed;
    }

    // Update is called once per frame
    void Update () {
        count++;
        if (count > 60 * 5)
            Destroy(this.gameObject);
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
