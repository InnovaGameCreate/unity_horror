using UnityEngine;
using System.Collections;

public class playersetfalse : MonoBehaviour {
   public GameObject player;
    public int dis=150;
        
    private Transform[] terrain;
    // Use this for initialization
    void Start () {
        terrain = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            terrain[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (Mathf.Abs(player.GetComponent<Transform>().position.x - terrain[i].position.x) > 100)
                terrain[i].gameObject.SetActive(false);
            else
                terrain[i].gameObject.SetActive(true);
        }
    }
}
