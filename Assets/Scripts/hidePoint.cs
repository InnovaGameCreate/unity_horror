using UnityEngine;
using System.Collections;

public class hidePoint : MonoBehaviour {

    private bool is_hiding=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")){
            //隠れる
            if (is_hiding==false&&Input.GetKeyDown(KeyCode.UpArrow))
            {

                is_hiding = true;
                other.gameObject.GetComponent<heromove>().InvincibleMode();
                Vector3 p = other.gameObject.transform.position;
                p =  new Vector3(p.x, p.y, 1.5f);
                other.gameObject.transform.position = p;
                other.gameObject.GetComponent<Animator>().SetFloat("Horizontal",0);
            }
            //表に出る
            else if(is_hiding == true&&Input.GetKeyDown(KeyCode.UpArrow))
            {
                is_hiding =false;
                other.gameObject.GetComponent<heromove>().OnFinishedInvincibleMode();
                Vector3 q = other.gameObject.transform.position;
                q =  new Vector3(q.x, q.y, 0);
                other.gameObject.transform.position = q;

            }
        }


    }

}
