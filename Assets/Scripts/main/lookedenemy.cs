using UnityEngine;
using System.Collections;

public class lookedenemy : MonoBehaviour
{
    private float iniy;
    public int lookedbyenemy;
    // Use this for initialization
    void Start()
    {
        iniy = GetComponent<Transform>().position.y;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 posi = GetComponent<Transform>().position;
        if (lookedbyenemy == 0)
        {
            gameObject.SetActive(false);

        }
        if(lookedbyenemy>0)
            gameObject.SetActive(true);
    }

    public void set_lookedbyenemyadd(int set)
    {
        lookedbyenemy += set;
    }
}
