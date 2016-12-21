using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    private NCMB.HighScore highScore;
    private bool isNewRecord;
    private int score;
    private bool flag = false;
    public Text scoreGUIText;
    public Text highScoreGUIText;

    void Start()
    {
        Initialize();
        score = lookenemycount.get_restcount();
        // ハイスコアを取得する。保存されてなければ0点。
        string name = FindObjectOfType<UserAuth>().currentPlayer();
        highScore = new NCMB.HighScore(0, name);
        highScore.fetch();
    }

    private void Initialize()
    {
        // スコアを0に戻す
        score = 0;
        // フラグを初期化する
        isNewRecord = false;
        flag = false;
    }
    // Update is called once per frame
    void Update()
    {

        // スコアがハイスコアより大きければ
        if (highScore.score < score)
        {
            isNewRecord = true; // フラグを立てる
            flag = true;
            highScore.score = score;
        }
        if (flag == true)
        {
            Save();
            flag = false;
        }
        // スコア・ハイスコアを表示する
        scoreGUIText.text = score.ToString();
        highScoreGUIText.text = "HighScore : " + highScore.score.ToString();


    }
    // ハイスコアの保存
    public void Save()
    {
        // ハイスコアを保存する（ただし記録の更新があったときだけ）
        if (isNewRecord)
            highScore.save();

        // ゲーム開始前の状態に戻す
       // Initialize();
    }
}
