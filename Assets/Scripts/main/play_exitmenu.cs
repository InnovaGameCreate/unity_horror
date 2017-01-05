using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class play_exitmenu : MonoBehaviour {

    private int selecting=0;
    private int maxselect = 3;
    AudioSource[] sound = new AudioSource[2];
    private float count;
    // Use this for initialization
    void Start () {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        for (int i = 0; i < 2; i++)
            sound[i] = audioSources[i];

    }
	
	// Update is called once per frame
	void Update () {
        //escメニューの選択
        if (selecting < maxselect - 1&& Input.GetKeyDown(KeyCode.DownArrow)) {
            sound[0].Play();
            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
            pos.y -=20;
            GetComponent<RectTransform>().anchoredPosition = pos;
            selecting++;
        }else if (selecting > 0 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            sound[0].Play();
            Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
            pos.y += 20;
            GetComponent<RectTransform>().anchoredPosition = pos;
            selecting--;
        }
        if (count==0&&Input.GetKeyDown(KeyCode.Return))
        {
            sound[1].Play();
            count++;
        }
        if (count > 0)
            count += Time.deltaTime;
        if (count > 1.1f)
        {
            switch (selecting)
            {
                case 0:
                    SceneManager.UnloadScene("main_escmenu");
                    Resources.UnloadUnusedAssets();
                    break;
                case 1:
                    SceneManager.UnloadScene("main_escmenu");
                    SceneManager.LoadScene(playerLife.scenename[heromove.nowstage]);
                    Resources.UnloadUnusedAssets();
                    break;
                case 2:
                    SceneManager.LoadScene("stage_select");
                    break;
            }
        }

    }
    //どれを選択してるか
   public int get_selecting()
    {
        return selecting;
    }
}
