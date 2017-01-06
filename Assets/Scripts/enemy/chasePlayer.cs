using UnityEngine;
using System.Collections;

public class chasePlayer : MonoBehaviour
{
    private bool find;
    private Collider samp;
    private RaycastHit wall;
    private const float RAY_LENGTH = 1.0f;
    // Use this for initialization
    void Start()
    {
        if (transform.parent.gameObject.GetComponent<enemyBase>().raylength > GetComponent<BoxCollider>().size.x / 2)
            transform.parent.gameObject.GetComponent<enemyBase>().raylength = GetComponent<BoxCollider>().size.x / 2 - 1;

    }
    // Update is called once per frame
    void Update()
    {
        ////前方
        //if (transform.parent.gameObject.GetComponent<enemyBase>().get_face() != 0)
        //    if (Physics.Raycast(this.transform.position, transform.parent.gameObject.GetComponent<enemyBase>().get_face() > 0 ? Vector3.left : Vector3.right, out wall, RAY_LENGTH) )

        //    {
        //        if (wall.collider.tag != "Ground")
        //        {
        if (find == true)
            transform.parent.gameObject.GetComponent<enemyBase>().chasePlayer(samp);
        //    }
        //}

    }


    //追跡範囲内 ※Updateと OnTriggerStay関連ではフレームレートが異なる
    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (transform.parent.gameObject.GetComponent<enemyBase>().get_findPlayer() == true)
            {
                find = true;
                if (samp == null)
                    samp = other;
            }
            else
                find = false;

        }

    }

    //追跡範囲外
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            transform.parent.gameObject.GetComponent<enemyBase>().set_findPlayer(false);
            find = false;
        }
    }

}