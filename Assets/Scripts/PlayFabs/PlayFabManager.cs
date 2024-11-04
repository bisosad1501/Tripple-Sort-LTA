using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
public class PlayFabManager : MonoBehaviour
{
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
       Debug.Log("Login with" + UseProfile.deviceId);
       if (result.InfoResultPayload.PlayerProfile != null)
       {
           if(result.InfoResultPayload.PlayerProfile.DisplayName != null)
           UseProfile.NamePlayer = result.InfoResultPayload.PlayerProfile.DisplayName;
           else
           {
               UseProfile.NamePlayer = UseProfile.deviceId;
           }
       }
       SendLeaderBoardScore(UseProfile.Star);
   }

   public void OnError(PlayFabError error)
   {
       Debug.LogError(error.GenerateErrorReport());
   }

   public void ChangeName(String newName)
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
       Debug.Log("Sent Score" + result);
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
   }
    public List<RankDataModel> leaderboardScores = new List<RankDataModel>();
   public void OnLeaderBoardScoreGet(GetLeaderboardResult result)
   {
       isLoadLeaderBoardDone = true;
       leaderboardScores = new List<RankDataModel>();
       foreach (var i in result.Leaderboard)
       {
           RankDataModel k = new RankDataModel();
           k.score = i.StatValue;
           if (i.DisplayName != null) k.userName = i.DisplayName;
           else k.userName = i.PlayFabId;
           k.rankID = i.Position + 1;
           leaderboardScores.Add(k);
       }
   }
}
