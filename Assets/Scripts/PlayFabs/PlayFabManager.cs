using System;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{
    public ErrorView errorView; 
    public List<RankDataModel> leaderboardScores = new List<RankDataModel>();
    public bool isLoadLeaderBoardDone; 

    public void Init()
    {
        Login();
    }

    public void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = UseProfile.deviceId,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    public void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login with " + UseProfile.deviceId);
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            UseProfile.NamePlayer = result.InfoResultPayload.PlayerProfile.DisplayName ?? UseProfile.deviceId;
        }
        GetLeaderBoardScores();
    }

    public void OnError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
        errorView?.OnError(error); 
    }

    public void ChangeName(string newName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = newName,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplaynameUpdated, OnError);
    }

    public void OnDisplaynameUpdated(UpdateUserTitleDisplayNameResult result)
    {
        UseProfile.NamePlayer = result.DisplayName;
        GameManager.Instance.uiManager.homeView.InitView();
        Debug.Log("Display name updated " + UseProfile.NamePlayer);
    }

    public void SendLeaderBoardScore(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Rank",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardScoreUpdated, OnError);
    }

    public void OnLeaderBoardScoreUpdated(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Sent Score " + result);
        SendEndTime();
    }

    public void SendEndTime()
    {
        int secondsSinceEpoch = (int)DateTime.Now.Subtract(new DateTime(2024, 1, 1)).TotalSeconds;
        Debug.Log("Seconds Since Epoch: " + secondsSinceEpoch);

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "timeplay",
                    Value = secondsSinceEpoch
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnEndTimeSent, OnError);
        Debug.Log("End Time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    public void OnEndTimeSent(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Sent End Time: " + result);
    }

    public void GetLeaderBoardScores()
    {
        isLoadLeaderBoardDone = false;

        var scoreRequest = new GetLeaderboardRequest
        {
            StatisticName = "Rank",
            StartPosition = 0,
            MaxResultsCount = 100
        };
        PlayFabClientAPI.GetLeaderboard(scoreRequest, OnLeaderBoardScoreGet, OnError);

        var timeRequest = new GetLeaderboardRequest
        {
            StatisticName = "timeplay",
            StartPosition = 0,
            MaxResultsCount = 100
        };
        PlayFabClientAPI.GetLeaderboard(timeRequest, OnLeaderBoardTimeGet, OnError);
    }

    public void OnLeaderBoardScoreGet(GetLeaderboardResult result)
    {
        leaderboardScores.Clear();

        foreach (var entry in result.Leaderboard)
        {
            RankDataModel rankData = new RankDataModel
            {
                score = entry.StatValue,
                userName = entry.DisplayName ?? entry.PlayFabId,
                rankID = entry.Position + 1,
                Time = "N/A"
            };
            leaderboardScores.Add(rankData);
        }
        isLoadLeaderBoardDone = true;
    }

    public void OnLeaderBoardTimeGet(GetLeaderboardResult result)
    {
        for (int i = 0; i < result.Leaderboard.Count; i++)
        {
            if (i < leaderboardScores.Count)
            {
                int secondsSinceEpoch = result.Leaderboard[i].StatValue;
                DateTime rankUpdateTime = new DateTime(2024, 1, 1).AddSeconds(secondsSinceEpoch);
                foreach (var k in leaderboardScores)
                {
                    if (k.userName == result.Leaderboard[i].DisplayName ||
                        k.userName == result.Leaderboard[i].PlayFabId)
                    {
                        k.Time = rankUpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                Debug.Log("Rank " + leaderboardScores[i].rankID + " was last updated at: " + leaderboardScores[i].Time);
            }
        }
    }
}

