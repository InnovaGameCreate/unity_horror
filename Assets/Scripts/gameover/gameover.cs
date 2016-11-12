using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameover : MonoBehaviour {
    private float count;
    private int appeartime = 3;
    public GameObject image;
    public GameObject select;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        count += Time.deltaTime;
        if (count < 5 || image.GetComponent<Image>().color.a > 0.01)
        {
            var color = image.GetComponent<Image>().color;
            color.a = (count < 3) ? color.a + 0.01f : color.a - 0.01f;
            image.GetComponent<Image>().color = color;
        }
        else
            select.SetActive(true);




    }
}
