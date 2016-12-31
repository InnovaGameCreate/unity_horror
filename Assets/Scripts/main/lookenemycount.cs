using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class lookenemycount : MonoBehaviour {
    public sanValueText sanText; //外部のsanValueTexオブジェクトを見えるよう定義
    private int restcount;
    public int lookcountmaxgameover = 20;
    public GameObject backtext;
    static private int lookcount;
    // Use this for initialization
    void Start () {
        restcount = lookcountmaxgameover;
	}
	static public int get_lookcount()
    {
        return lookcount;
    }
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = backtext.GetComponent<Text>().text= restcount.ToString();
        float alookminus = (float)(lookcountmaxgameover- restcount) /(float)lookcountmaxgameover;
        float setreal = sanText.get_sanmax() - sanText.get_sanmax() * alookminus;
        if (setreal <= 0)
            setreal = 1;
        sanText.set_sanrealmax(setreal);

        lookcount = lookcountmaxgameover-restcount;
    }


   public void addlookcount()
    {
        if(restcount > 0)
            restcount--;
    
    }
}
