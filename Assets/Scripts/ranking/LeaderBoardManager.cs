using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderBoardManager : MonoBehaviour
{

    private LeaderBoard lBoard;
    private NCMB.HighScore currentHighScore;
    public GameObject[] top = new GameObject[5];
    public GameObject[] nei = new GameObject[5];
    public GameObject[] myrank = new GameObject[3];
    public GameObject[] topc = new GameObject[5];
    public GameObject[] topt = new GameObject[5];
    public GameObject[] neic = new GameObject[5];
    public GameObject[] neit = new GameObject[5];
    bool isScoreFetched;
    bool isRankFetched;
    bool isLeaderBoardFetched;

    // ボタンが押されると対応する変数がtrueになる
    private bool backButton;
    private float count;
    void Start()
    {
     

        // テキストを表示するゲームオブジェクトを取得
        for (int i = 0; i < 5; ++i)
        {
            top[i] = GameObject.Find("Top" + i);
            nei[i] = GameObject.Find("Neighbor" + i);
        }

        initialize();
    }
    void initialize()
    {
        lBoard = new LeaderBoard();
        // フラグ初期化
        isScoreFetched = false;
        isRankFetched = false;
        isLeaderBoardFetched = false;

        // 現在のハイスコアを取得
        string name = FindObjectOfType<UserAuth>().currentPlayer();
        currentHighScore = new NCMB.HighScore(-1,-1, name);
        currentHighScore.fetch();
    }

    void Update()
    {
        count += Time.deltaTime;
   
        // 現在のハイスコアの取得が完了したら1度だけ実行
        if (currentHighScore.score != -1 && currentHighScore.time != -1&& !isScoreFetched)
        {
            lBoard.fetchRank(currentHighScore.score, currentHighScore.time);
            isScoreFetched = true;
        }

        // 現在の順位の取得が完了したら1度だけ実行
        if (lBoard.currentRank != 0 && !isRankFetched)
        {
            lBoard.fetchTopRankers();
            lBoard.fetchNeighbors();
            isRankFetched = true;
        }

        // ランキングの取得が完了したら1度だけ実行
        if (lBoard.topRankers != null && lBoard.neighbors != null && !isLeaderBoardFetched)
        {
            // 自分が1位のときと2位のときだけ順位表示を調整
            int offset = 2;
            if (lBoard.currentRank == 1)
            {
                offset = 0;
                myrank[0].GetComponent<Image>().enabled = true;
            }
           else if (lBoard.currentRank == 2)
            {
                offset = 1;
                myrank[1].GetComponent<Image>().enabled = true;
            }else
                myrank[2].GetComponent<Image>().enabled = true;
            Debug.Log(lBoard.currentRank);
            // 取得したトップ5ランキングを表示
            for (int i = 0; i < lBoard.topRankers.Count; ++i)
            {
                top[i].GetComponent<Text>().text = i + 1 + "位  " + lBoard.topRankers[i].print();
                topc[i].GetComponent<Text>().text = lBoard.topRankers[i].score != 100 ? lBoard.topRankers[i].print_count() : "";
                topt[i].GetComponent<Text>().text = lBoard.topRankers[i].time != 60 * 10 ? lBoard.topRankers[i].print_time() : "未登録";
            }

            // 取得したライバルランキングを表示
            for (int i = 0; i < lBoard.neighbors.Count; ++i)
            {
                nei[i].GetComponent<Text>().text = lBoard.currentRank - offset + i + "位  " + lBoard.neighbors[i].print();
                neic[i].GetComponent<Text>().text =  lBoard.neighbors[i].score!=100? lBoard.neighbors[i].print_count():"";
                neit[i].GetComponent<Text>().text = lBoard.neighbors[i].time != 60*10 ? lBoard.neighbors[i].print_time():"未登録";
            }
            Debug.Log("lBoard.neighbors.Count" + lBoard.neighbors.Count);
            isLeaderBoardFetched = true;
        }

        if ((count > 2) && (top[0].GetComponent<Text>().text == "NowLoading..." || nei[0].GetComponent<Text>().text == "NowLoading..."))
        {

            initialize();
            count = 0;
        }
    }

    //void OnGUI()
    //{
    //    drawMenu();
    //    // 戻るボタンが押されたら
    //    if (backButton)
    //    {

    //            Application.LoadLevel("gameclear");

    //    }
    //}

    //private void drawMenu()
    //{
    //    // ボタンの設置
    //    int btnW = 170, btnH = 30;
    //    GUI.skin.button.fontSize = 20;
    //    backButton = GUI.Button(new Rect(Screen.width * 1 / 2 - btnW * 1 / 2, Screen.height * 7 / 8 - btnH * 1 / 2, btnW, btnH), "Back");
    //}
}