﻿using NCMB;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard
{

    public int currentRank = 0;
    public List<NCMB.HighScore> topRankers = null;
    public List<NCMB.HighScore> neighbors = null;

    // 現プレイヤーのハイスコアを受けとってランクを取得 ---------------
    public void fetchRank(int currentScore, int currentTime)
    {
        // データスコアの「HighScore」から検索
        NCMBQuery<NCMBObject> rankQuery = new NCMBQuery<NCMBObject>("HighScore" + (int)stage_select.stage_is);
        rankQuery.OrderByAscending("Total");
   
        rankQuery.WhereLessThanOrEqualTo("Total", currentScore*1000+ currentTime);
     
        rankQuery.CountAsync((int count, NCMBException e) => {

            if (e != null)
            {
                //件数取得失敗
            }
            else
            {
                //WhereLessThanOrEqualToのときは1から数えてくれるので count
                //WhereGreaterThanOrEqualToのときは0から数えるのでcount+1
                //件数取得成功
                currentRank = count; // 自分よりスコアが上の人がn人いたら自分はn+1位
                Debug.Log("count"+count);
            }
        });

    }

    // サーバーからトップ5を取得 ---------------    
    public void fetchTopRankers()
    {
        // データストアの「HighScore」クラスから検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore" + (int)stage_select.stage_is);

        query.OrderByAscending("Total");

        // query.OrderByDescending("Score");
        query.Limit = 5;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {

            if (e != null)
            {
                //検索失敗時の処理
            }
            else
            {
                //検索成功時の処理
                List<NCMB.HighScore> list = new List<NCMB.HighScore>();
                // 取得したレコードをHighScoreクラスとして保存
                foreach (NCMBObject obj in objList)
                {
                    int s = System.Convert.ToInt32(obj["Score"]);
                    string n = System.Convert.ToString(obj["Name"]);
                    int t = System.Convert.ToInt32(obj["Time"]);
                    list.Add(new HighScore(s,t, n));
                }
                topRankers = list;
            }
        });
    }

    // サーバーからrankの前後2件を取得 ---------------
    public void fetchNeighbors()
    {
        neighbors = new List<NCMB.HighScore>();

        // スキップする数を決める（ただし自分が1位か2位のときは調整する）
        int numSkip = currentRank - 3;
        if (numSkip < 0) numSkip = 0;

        // データストアの「HighScore」クラスから検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore" + (int)stage_select.stage_is);

        query.OrderByAscending("Total");
        // query.OrderByDescending("Score");
        query.Skip = numSkip;
        query.Limit = 5;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {

            if (e != null)
            {
                //検索失敗時の処理
            }
            else
            {
                //検索成功時の処理
                List<NCMB.HighScore> list = new List<NCMB.HighScore>();
                // 取得したレコードをHighScoreクラスとして保存
                foreach (NCMBObject obj in objList)
                {
                    int s = System.Convert.ToInt32(obj["Score"]);
                    string n = System.Convert.ToString(obj["Name"]);
                    int t = System.Convert.ToInt32(obj["Time"]);
                    list.Add(new HighScore(s,t, n));
                }
                neighbors = list;
            }
        });
    }
}