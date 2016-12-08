using UnityEngine;
using System.Collections;

public class touchGround : MonoBehaviour {
    heromove move;
	// Use this for initialization
	void Start () {
        move = transform.root.gameObject.GetComponent<heromove>();

    }
	
	// Update is called once per frame
	void Update () {
    
    }

    //// オブジェクトと接触した時に呼ばれるコールバック
    //void OnTriggerEnter(Collider other)
    //{


    //    if (!move.get_is_ground() && other.CompareTag("Ground"))
    //    {
   
    //        //衝突したのが地形(Terrain)だったら接地したと判断
    //        move.set_is_ground(true);
    //        transform.root.gameObject.GetComponent<Animator>().SetBool("isGround", move.get_is_ground());
        
    //    }
    //}
    void OnTriggerStay(Collider other)
    {


        if (!move.get_is_ground() && other.CompareTag("Ground"))
        {

            //衝突したのが地形(Terrain)だったら接地したと判断
            move.set_is_ground(true);
          //  transform.root.gameObject.GetComponent<Animator>().SetBool("isGround", move.get_is_ground());

        }
    }

    void OnTriggerExit(Collider other)
    {
        move.set_is_ground(false);
    }

    }
