using UnityEngine;
using System.Collections;

public class findPlayer : MonoBehaviour
{
    public heromove state_info;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    //索敵範囲内 敵を見つける
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (transform.root.gameObject.GetComponent<groundEnemy>() != null)
            {
                transform.root.gameObject.GetComponent<groundEnemy>().set_findPlayer(true);
                if (state_info.get_state() == heromove.State.Invincible)
                    transform.root.gameObject.GetComponent<groundEnemy>().set_findPlayer(false);
            }else
            {
                transform.root.gameObject.GetComponent<flyEnemy>().set_findPlayer(true);
                if (state_info.get_state() == heromove.State.Invincible)
                    transform.root.gameObject.GetComponent<flyEnemy>().set_findPlayer(false);
            }
        }
    }
}
