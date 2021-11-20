using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using AbductionCar.Framework;
using System;

namespace AbductionCar.Managers
{
    public class RankingManager : SingletonMonoBehaviour<RankingManager>
    {
        public string className = "Ranking";
        public int limitCount = 20;
        private List<RankingData> rankingDataList = new List<RankingData>();
        public List<RankingData> GetRanking() { return rankingDataList; }
        public bool IsRankingDataValid { get; private set; }
        private string currentObjectId;
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this.gameObject);
        }

        public void FetchRanking()
        {
            if(CheckNetworkValid() == false)
            {
                return;
            }

            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(className);
            query.OrderByDescending("Score");
            query.Limit = limitCount;
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {
                if (e != null)
                {
                    IsRankingDataValid = false;
                    Debug.Log("ランキング取得：取得失敗");
                }
                else
                {
                    int rank = 1;
                    foreach (NCMBObject obj in objList)
                    {
                        rankingDataList.Add(new RankingData(
                            rank++,
                            Convert.ToString(obj["Name"]),
                            Convert.ToInt32(obj["Score"]),
                            obj.ObjectId
                            ));
                    }
                    IsRankingDataValid = true;
                }
            });
        }

        public void SaveRanking(string name, int score)
        {
            if (CheckNetworkValid() == false || score <= 0)
            {
                return;
            }

            NCMBObject ncmbObject = new NCMBObject(className);

            if (string.IsNullOrEmpty(name))
            {
                name = "No Name";
            }

            // オブジェクトに値を設定
            ncmbObject["Name"] = name;
            ncmbObject["Score"] = score;

            // データストアへの登録
            ncmbObject.SaveAsync((NCMBException e) =>
            {
                if (e != null)
                {
                    Debug.Log("ランキング保存：接続失敗");
                }
                else
                {
                    currentObjectId = ncmbObject.ObjectId;
                }
                FetchRanking();
            });
        }

        private bool CheckNetworkValid()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogWarning("ネットワーク接続なし");
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class RankingData
    {
        public readonly int rankNum;
        public readonly string name;
        public readonly int score;
        public readonly string objectId;

        public RankingData(int rankNum, string name, int score, string objectId)
        {
            this.rankNum = rankNum;
            this.name = name;
            this.score = score;
            this.objectId = objectId;
        }
    }
}