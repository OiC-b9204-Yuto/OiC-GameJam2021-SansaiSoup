using AbductionCar.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AbductionCar.UI
{
    public class RankingMenu : MonoBehaviour
    {
        [SerializeField] GameObject rankingPanel;

        [SerializeField] private Selectable enableSelectedObejct;
        [SerializeField] private Selectable disableSelectedObejct;

        [SerializeField] private RankingObejct cloneRankingObect;
        List<RankingObejct> rankingObjectList = new List<RankingObejct>();
        [SerializeField] private Transform rankingContent;

        [SerializeField] private CustomButton refreshButton;
        float refreshCooldownTime = 0;


        void Start()
        {
            for (int i = 0; i < RankingManager.Instance.limitCount; i++)
            {
                var obj = Instantiate(cloneRankingObect.gameObject, rankingContent).GetComponent<RankingObejct>();
                rankingObjectList.Add(obj);
            }
        }

        void Update()
        {
            if (refreshCooldownTime > 0)
            {
                refreshCooldownTime -= Time.deltaTime;
            }
            else
            {
                refreshButton.interactable = true;
            }
        }

        public void RankingRefresh()
        {
            RankingManager.Instance.FetchRanking();
            if (RankingManager.Instance.IsRankingDataValid)
            {
                List<RankingData> rankingDataList = RankingManager.Instance.GetRanking();
                for (int i = 0; i < RankingManager.Instance.limitCount; i++)
                {
                    if (i < rankingDataList.Count)
                    {
                        rankingObjectList[i].SetValue(rankingDataList[i]);
                    }
                    else
                    {
                        rankingObjectList[i].ValueClear();
                    }
                }
                refreshCooldownTime = 30;
                refreshButton.interactable = false;
            }
        }

        public void Enable()
        {
            if (enableSelectedObejct)
            {
                enableSelectedObejct.Select();
            }
            if (refreshCooldownTime <= 0)
            {
                RankingRefresh();
            }
            rankingPanel.SetActive(true);
        }

        public void Disable()
        {
            if (disableSelectedObejct)
            {
                disableSelectedObejct.Select();
            }
            rankingPanel.SetActive(false);
        }

        public bool IsActive()
        {
            return rankingPanel.activeSelf;
        }
    }
}