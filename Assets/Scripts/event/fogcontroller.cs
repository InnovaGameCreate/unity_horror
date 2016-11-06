using UnityEngine;
using System.Collections;

public class fogcontroller : MonoBehaviour
{

    public bool right_or_left=true;      //霧が出始めるのは右に移動する時か左に移動するときかかどうか（右ならチェックつける)

    public Color color = Color.gray;
    public float maxdensity = 0.5f;     //霧の最終的な最大濃度
    public float mindensity = 0;     //霧の最終的な最小濃度       //注意）複数設置するときはこの濃度のつじつまが合うようにする

    private float colliderdis;
    private bool hit;
    GameObject samp;

    void Start()
    {
        colliderdis = GetComponent<BoxCollider>().size.x;
    
    }

    // Update is called once per frame
    void Update()
    {
  
        if (samp != null&&hit==true)
        {
            float leftest = GetComponent<Transform>().position.x - colliderdis / 2;
            float rightest = GetComponent<Transform>().position.x + colliderdis / 2;
            float hero = samp.GetComponent<Transform>().position.x;

    
            if (leftest <= hero && hero <= rightest)
            {
                if (right_or_left)
                {
                    float sa = samp.GetComponent<Transform>().position.x - leftest;
                    RenderSettings.fogDensity = maxdensity * (sa / colliderdis)+mindensity;

                }
                else
                {
                    float sa2 = -(samp.GetComponent<Transform>().position.x - rightest);
                    RenderSettings.fogDensity = maxdensity * (sa2 / colliderdis) + mindensity;

                }
            }

        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hit = true;
            if (samp == null)
                samp = other.gameObject;
       
            RenderSettings.fogColor = Color.gray;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hit = false;

        }
    }
}
