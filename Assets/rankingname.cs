using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class rankingname : MonoBehaviour {

    public GameObject inputname;

	// Use this for initialization
	void Start () {

    }
    void Update()
    {
        if (FindObjectOfType<UserAuth>().currentPlayer() != null)
            SceneManager.LoadScene("stage_select");
    }            
   public void deciderankname()
    {
        string name = inputname.GetComponent<InputField>().text;
        SaveData.SetString("rankname", name);
   
        if (name == "")
            return;
        UserAuth.Instance.AnonymousSignup();
        // currentPlayerを毎フレーム監視し、ログインが完了したら

    }
}
