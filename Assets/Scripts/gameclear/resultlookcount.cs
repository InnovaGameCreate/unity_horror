using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class resultlookcount : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<Text>().text ="名状しがたきものを見た回数："+ lookenemycount.get_lookcount().ToString()+"回";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
