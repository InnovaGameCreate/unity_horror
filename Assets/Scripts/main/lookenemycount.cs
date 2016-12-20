using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class lookenemycount : MonoBehaviour {
    public sanValueText sanText; //外部のsanValueTexオブジェクトを見えるよう定義
    private int lookcount;
    public int lookcountmaxgameover = 20;
    static private int restcount;
    // Use this for initialization
    void Start () {
        lookcount = lookcountmaxgameover;
	}
	static public int get_restcount()
    {
        return restcount;
    }
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = lookcount.ToString();
        float alookminus = (float)(lookcountmaxgameover- lookcount )/(float)lookcountmaxgameover;
        float setreal = sanText.get_sanmax() - sanText.get_sanmax() * alookminus;
        if (setreal <= 0)
            setreal = 1;
        sanText.set_sanrealmax(setreal);

        restcount = lookcountmaxgameover-lookcount;
    }


   public void addlookcount()
    {
        if(lookcount>0)
        lookcount--;
    
    }
}
