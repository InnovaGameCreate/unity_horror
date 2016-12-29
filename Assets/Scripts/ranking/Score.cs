using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private NCMB.HighScore highScore;
    private bool isNewRecord;
    private int score, time;
    private bool flag = false;
    public Text stage;
    public Text scoreGUIText;
    public Text timeGUIText;
    public Text highScoreGUIText;
    public Text highTimeGUIText;
    private float count;
    void Start()
    {
        Initialize();
        score = lookenemycount.get_lookcount();
        time = (int)timercount.usedtime;
        // ハイスコアを取得する。保存されてなければ0点。
        string name = FindObjectOfType<UserAuth>().currentPlayer();
        highScore = new NCMB.HighScore(-1,-1, name);
        highScore.fetch();
        stage.text = "ステージ" + (int)heromove.nowstage;

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
        count += Time.deltaTime;
        // スコアがハイスコアより小さければ
        if (highScore.score > score)
        {
            isNewRecord = true; // フラグを立てる
            flag = true;
            highScore.score = score;
            highScore.time = time;
        }
        else if (highScore.score == score && highScore.time > time)
        {
            isNewRecord = true; // フラグを立てる
            flag = true;
            highScore.time = time;
        }
        if (flag == true)
        {
            Save();
            flag = false;
        }
        // スコア・ハイスコアを表示する
        scoreGUIText.text = "名状しがたきものを見た回数　" + score.ToString() + "回";
        timeGUIText.text = "経過時間　" + ((int)(time / 60)).ToString() + "分" + ((int)time % 60).ToString() + "秒";
        if (highScore.score != 100 && count > 1)
            highScoreGUIText.text = "名状しがたきものを見た回数　" + highScore.score.ToString() + "回";
        if (highScore.time != 60*10 && count > 1)
            highTimeGUIText.text = "経過時間　" + ((int)(highScore.time / 60)).ToString() + "分" + ((int)highScore.time % 60).ToString() + "秒";
    }
    // ハイスコアの保存
    public void Save()
    {
        // ハイスコアを保存する（ただし記録の更新があったときだけ）
        if (isNewRecord)
            highScore.save();

        // ゲーム開始前の状態に戻す
        //  Initialize();
    }
}
