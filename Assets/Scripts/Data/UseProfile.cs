using UnityEngine;
using System;

public class UseProfile : MonoBehaviour

{
    #region ID
    public static string deviceId
    {
        get => SystemInfo.deviceUniqueIdentifier;
    }
    #endregion
    
    
    public static string NamePlayer
    {
        get
        {
            return PlayerPrefs.GetString("FB_NAME_v2", deviceId);
        }
        set
        {
            PlayerPrefs.SetString("FB_NAME_v2", value);
            PlayerPrefs.Save();
        }
    }
   
   
    
   

   
    
   
    public static int CurrentLevel
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CURRENT_LEVEL, 1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_LEVEL, value);
            PlayerPrefs.Save();
        }
    }
   
    public int CurrentLevelPlay
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CURRENT_LEVEL_PLAY, 1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_LEVEL_PLAY, value);
            PlayerPrefs.Save();
        }
    }
    
   

   
   

   
    public static int XpRank
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.XpRank, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.XpRank, value);
            PlayerPrefs.Save();
        }
    } 
    public static int RewardVictoryProgress
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.RewardVictoryProgress, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.RewardVictoryProgress, value);
            PlayerPrefs.Save();
        }
    }

    

   


    public int CurrentBranchSkin
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CURRENT_BRANCH_SKIN, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_BRANCH_SKIN, value);
            PlayerPrefs.Save();
        }
    }

    public string OwnedBranchSkin
    {
        get
        {
            return PlayerPrefs.GetString(StringHelper.OWNED_BRANCH_SKIN, "");
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.OWNED_BRANCH_SKIN, value);
            PlayerPrefs.Save();
        }
    }
   
    public int CurrentTheme
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CURRENT_THEME, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_THEME, value);
            PlayerPrefs.Save();
        }
    }
    public string OwnedThemeSkin
    {
        get
        {
            return PlayerPrefs.GetString(StringHelper.OWNED_THEME, "");
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.OWNED_THEME, value);
            PlayerPrefs.Save();
        }
    }

   

   

    
    public static int Coin
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.Coin, 100);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.Coin, value);
            PlayerPrefs.Save();
        }
    }
    public static int Star
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.Star, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.Star, value);
            GameController.Instance.playFabManager.SendLeaderBoardScore(value);
            PlayerPrefs.Save();
        }
    }
    public static int Heart
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.Heart, 5);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.Heart, value);
            PlayerPrefs.Save();
        }
    }
    public static int Hint
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.Hint, 1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.Hint, value);
            PlayerPrefs.Save();
        }
    }
    public static int Shuffle
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.Shuffle, 1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.Shuffle, value);
            PlayerPrefs.Save();
        }
    }
    public static int Freeze
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.Freeze, 1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.Freeze, value);
            PlayerPrefs.Save();
        }
    }
    public static int LuckySpinProgress
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.LuckySpinProgress, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.LuckySpinProgress, value);
            PlayerPrefs.Save();
        }
    }

   
}

