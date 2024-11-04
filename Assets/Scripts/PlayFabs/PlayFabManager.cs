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
           UseProfile.NamePlayer = result.InfoResultPayload.PlayerProfile.DisplayName;
       }
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
}
