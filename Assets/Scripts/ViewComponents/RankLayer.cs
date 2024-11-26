using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankLayer : SliderNode
{
    public List<RankDataModel> rankingDataList = new List<RankDataModel>();
    public List<RankItem> rankItemList = new List<RankItem>();

    public CanvasGroup allTab;

    public RankItem rankItemPrefab;

    public RectTransform allTabRoot;

    public GameObject allTabSelectObj;

    public Text myRankingTxt;

    public Text myUserNameTxt;

    public Text myScoreTxt;

    private bool loaded;

    public override void InitView()
    {
        myUserNameTxt.text = "Your Ranking";
        myScoreTxt.text = GameManager.Instance.currentStar.ToString();
        StartCoroutine(LoadingView());
    }

    void Start()
    {
        loaded = false;
        StartCoroutine(LoadingView());
        ChooseAllTab();
    }

    IEnumerator LoadingView()
    {
        GameController.Instance.playFabManager.GetLeaderBoardScores();
        yield return new WaitUntil(() => GameController.Instance.playFabManager.isLoadLeaderBoardDone);
        rankingDataList = GameController.Instance.playFabManager.leaderboardScores;
        LoadView();
    }

    private bool isPlayerin100;
    private void LoadView()
    {
        // Xóa các item trước đó
        for (int i = 0; i < rankItemList.Count; i++)
        {
            Destroy(rankItemList[i].gameObject);
        }
        rankItemList.Clear();

        int rankingCount = rankingDataList.Count;
        if (rankingCount > GameManager.Instance.gamePlaySetting.rankingSize) 
            rankingCount = GameManager.Instance.gamePlaySetting.rankingSize;

        for (int i = 0; i < rankingCount; i++)
        {
            RankItem item = Instantiate(rankItemPrefab);
            rankItemList.Add(item);
            item.ShowView(rankingDataList[i]);
            item.transform.SetParent(allTabRoot);
            item.transform.localScale = Vector3.one;

            if (rankingDataList[i].userName.CompareTo(UseProfile.NamePlayer) == 0)
            {
                myRankingTxt.text = rankingDataList[i].rankID.ToString();    
                isPlayerin100 = true;
            }
        }

        if (!isPlayerin100) 
            myRankingTxt.text = "100+";
    }

    public void ChooseAllTab()
    {
        if (loaded)
            AudioManager.instance.btnSound.Play();

        allTab.alpha = 1.0f;
        allTab.interactable = true;
        allTab.blocksRaycasts = true;
        allTabSelectObj.SetActive(true);

        loaded = true;
    }
}