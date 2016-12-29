using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class rankingname : MonoBehaviour {

    public GameObject inputname;
    static public string resistname;
    private bool buttoned;
    public GameObject usednametext;
    private float count,co;
	// Use this for initialization
	void Start () {

    }
    void Update()
    {


        if (buttoned == true)
            co += Time.deltaTime;
        // currentPlayerを毎フレーム監視し、ログインが完了したら
        if (FindObjectOfType<UserAuth>().currentPlayer() != "")
        {
            SaveData.SetString("rankname", resistname);
            SceneManager.LoadScene("stage_select");
        }else if (buttoned == true&&co>2)
        {
            count += Time.deltaTime;
            if (count < 0.1f)
                usednametext.GetComponent<Text>().enabled = true;
            else if (count <0.2f)
                usednametext.GetComponent<Text>().enabled = false;
            else if (count < 0.3f)
            {
                usednametext.GetComponent<Text>().enabled = true;
                count = 0;
                co = 0;
                buttoned = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            deciderankname();
        }
    }            
   public void deciderankname()
    {
        string name = inputname.GetComponent<InputField>().text;
     

   
        if (name == "")
            return;
        resistname = name;
        UserAuth.Instance.AnonymousSignup();
        buttoned = true;
    

    }
}
