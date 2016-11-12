using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class goalPoint : MonoBehaviour {
    public playerLife goal;     //lifevaluetextを指定すること

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        //敵と接触時
        if (other.CompareTag("Player"))
        {
            goal.set_flag(false);
            SceneManager.LoadScene("gameclear");
        }

    }

}
