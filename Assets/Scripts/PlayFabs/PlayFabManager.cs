using System;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{
    public ErrorView errorView; 

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
        SendLeaderBoardScore(UseProfile.Star);
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
    }

    public bool isLoadLeaderBoardDone; 

    public void GetLeaderBoardScores()
    {
        isLoadLeaderBoardDone = false;
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Rank",
            StartPosition = 0,
            MaxResultsCount = 100
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardScoreGet, OnError);
        var tmprequest = new GetLeaderboardRequest
        {
            StatisticName = "timeplay",
            StartPosition = 0,
            MaxResultsCount = 100
        };
        PlayFabClientAPI.GetLeaderboard(tmprequest, OnLeaderBoardTimeGet, OnError);
    }

    public List<RankDataModel> leaderboardScores = new List<RankDataModel>();

    public void OnLeaderBoardScoreGet(GetLeaderboardResult result)
    {
        isLoadLeaderBoardDone = true;
        leaderboardScores = new List<RankDataModel>();
        foreach (var i in result.Leaderboard)
        {
            RankDataModel k = new RankDataModel
            {
                score = i.StatValue,
                userName = i.DisplayName ?? i.PlayFabId,
                rankID = i.Position + 1
            };
            leaderboardScores.Add(k);
        }
    }
    public void OnLeaderBoardTimeGet(GetLeaderboardResult result)
    {
        isLoadLeaderBoardDone = true;
        for (int i = 0; i < result.Leaderboard.Count; i++)
        {
            DateTime rankUpdateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((result.Leaderboard[i].StatValue));
            Debug.Log("Rank was last updated at: " + rankUpdateTime.ToString());
            leaderboardScores[i].Time = rankUpdateTime.ToString();
        }
    }

    // New method to send playtime to PlayFab
    public void SendPlayTimeToPlayFab()
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "timeplay",
                    Value = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnPlayTimeSent, OnError);
    }

    public void OnPlayTimeSent(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Sent PlayTime " + result);
    }
}