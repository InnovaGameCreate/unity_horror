using NCMB;
using System.Collections.Generic;

namespace NCMB
{
    public class HighScore
    {
        public int time { get; set; }
        public int score { get; set; }
        public string name { get; private set; }

        // コンストラクタ -----------------------------------
        public HighScore(int _score,int _time, string _name)
        {
            score = _score;
            name = _name;
            time = _time;
        }

        // サーバーにハイスコアを保存 -------------------------
        public void save()
        {
            // データストアの「HighScore」クラスから、Nameをキーにして検索
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore"+ (int)stage_select.stage_is);
            query.WhereEqualTo("Name", name);
            query.FindAsync((List<NCMBObject> objList, NCMBException e) => {

                //検索成功したら    
                if (e == null)
                {
                    objList[0]["Score"] = score;
                    objList[0]["Time"] = time;
                    objList[0]["Total"] = score*1000+time;
                    objList[0].SaveAsync();
                }
            });
        }

        // サーバーからハイスコアを取得  -----------------
        public void fetch()
        {
            // データストアの「HighScore」クラスから、Nameをキーにして検索
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore"+ (int)stage_select.stage_is);
            query.WhereEqualTo("Name", name);
            query.FindAsync((List<NCMBObject> objList, NCMBException e) => {

                //検索成功したら  
                if (e == null)
                {
                    // ハイスコアが未登録だったら
                    if (objList.Count == 0)
                    {
                        NCMBObject obj = new NCMBObject("HighScore"+ (int)stage_select.stage_is);
                        obj["Name"] = name;
                        obj["Score"] = 100;                //許容目視回数100回以上のマップは作らないこと
                        obj["Time"] =60*10;        //残り時間10分以上のマップは作らないこと
                        obj["Total"] = 100*1000+60 * 10;
                        obj.SaveAsync();
                        score = 100;
                        time= 60 * 10;
                    }
                    // ハイスコアが登録済みだったら
                    else
                    {
                        score = System.Convert.ToInt32(objList[0]["Score"]);
                        time = System.Convert.ToInt32(objList[0]["Time"]);
                    }
                }
            });
        }

        // ランキングで表示するために文字列を整形 -----------
        public string print()
        {
            return name ;
        }
        public string print_count()
        {
            return score + "回" ;
        }
        public string print_time()
        {
            return  time + "秒";
        }


    }

}