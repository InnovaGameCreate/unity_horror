using UnityEngine;
using System.Collections;

public class appearfoot : MonoBehaviour {

    private Transform[] foots;
    private float count;
    public int stagenofoot;
    public GameObject nextpoing;
    // Use this for initialization
    void Start () {
        foots = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            foots[i] = transform.GetChild(i);
            if (goalPoint.newclear != stagenofoot && SaveData.GetInt("stage" + stagenofoot.ToString()) == 1)
            {
                foots[i].gameObject.SetActive(true);
                 nextpoing.SetActive(true);
            }
        }



    }
	
    bool appear()
    {
        bool finish = false;
        count+=Time.deltaTime;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i  + 1 < count)
            {
                foots[i].gameObject.SetActive(true);
                if (i == transform.childCount - 1)
                    finish = true;
            }
        }


        return finish;
    }
	// Update is called once per frame
	void Update () {
        if (goalPoint.newclear == stagenofoot)
        {
            if (appear() == true)
            {
               goalPoint.newclear = 0;
                nextpoing.SetActive(true);
            }
        }

    }
}
