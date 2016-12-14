using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class lookenemycount : MonoBehaviour {
    private int lookcount;
	// Use this for initialization
	void Start () {
        lookcount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = lookcount.ToString();
    }

   public void addlookcount()
    {
        lookcount++;
    }
}
