using UnityEngine;
using System.Collections;

public class lookedenemy : MonoBehaviour
{
    private Vector2[] ini;
    private float range = 63.3f - 39.4014f;
    public int lookedbyenemy;
    private Transform[] img;
    // Use this for initialization
    //まとめたgameobjectではRectTransformは取得できない模様
    void Start()
    {
        img = new Transform[transform.childCount];
        ini = new Vector2[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            img[i] = transform.GetChild(i);
            ini[i] = img[i].GetComponent<RectTransform>().anchoredPosition;
            //   img[i].transform.gameObject.SetActive(false);
        }

       
      
    }

    // Update is called once per frame
    void Update()
    {
        float[] posi =new float[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            posi[i]= img[i].GetComponent<RectTransform>().anchoredPosition.y;
        }
     
        if (lookedbyenemy == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (ini[i].y < img[i].GetComponent<RectTransform>().anchoredPosition.y)
                    posi[i] = posi[i] > ini[i].y ? posi[i] - 1 : ini[i].y;
            }
            //for (int i = 0; i < transform.childCount; i++)
            //    img[i].transform.gameObject.SetActive(false);
        }
        else if (lookedbyenemy > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (ini[i].y+range > img[i].GetComponent<RectTransform>().anchoredPosition.y)
                    posi[i] = posi[i] < ini[i].y + range ? posi[i] + 1 : ini[i].y + range;
            }
            //for (int i = 0; i < transform.childCount; i++)
            //    img[i].transform.gameObject.SetActive(true);

        }
        for (int i = 0; i < transform.childCount; i++)
                 img[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(ini[i].x, posi[i]);
        

    }

    public void set_lookedbyenemyadd(int set)
    {
        lookedbyenemy +=set;
    }
    public int get_lookedbyenemy()
    {
        return lookedbyenemy;
    }
}
