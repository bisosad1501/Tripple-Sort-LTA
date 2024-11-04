using UnityEngine;
using System;

public class UseProfile : MonoBehaviour

{
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
    public static string PlayFabId
    {
        get
        {
            return PlayerPrefs.GetString("PlayFabId", string.Empty);
        }
        set
        {

            PlayerPrefs.SetString("PlayFabId", value);
            PlayerPrefs.Save();
        }
    }
    public static string Country_Code
    {
        get
        {
            return PlayerPrefs.GetString("country_code");
        }
        set
        {
            PlayerPrefs.SetString("country_code", value);
        }
    }
    #region ID
    public static string deviceId
    {
        get => SystemInfo.deviceUniqueIdentifier;
    }
    #endregion
    public static int _minimumLevel
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MinimumLevelInter, 2);
        }
    }

    public static int _miniumInterval
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MiniumInterval, 90);
        }
    }
    
    public static bool GDPRContentValue
    {
        get => PlayerPrefs.GetInt(StringHelper.GDPRContentValue, 1) == 1;
        set
        {
            PlayerPrefs.SetInt(StringHelper.GDPRContentValue, value ? 1 : 0);
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
    public static string _CurrentLevelMode => "Normal";
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
    
    public static bool IsRemoveAds
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.REMOVE_ADS, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.REMOVE_ADS, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public bool OnVibration
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ONOFF_VIBRATION, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ONOFF_VIBRATION, value ? 1 : 0);
        }
    }

    public bool OnSound
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ONOFF_SOUND, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ONOFF_SOUND, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public bool OnMusic
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ONOFF_MUSIC, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ONOFF_MUSIC, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool IsFirstTimeInstall
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.FIRST_TIME_INSTALL, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.FIRST_TIME_INSTALL, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static int RetentionD
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.RETENTION_D, 0);
        }
        set
        {
            if (value < 0)
                value = 0;

            PlayerPrefs.SetInt(StringHelper.RETENTION_D, value);
            PlayerPrefs.Save();
        }
    }

    public static int DaysPlayed
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.DAYS_PLAYED, 1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.DAYS_PLAYED, value);
            PlayerPrefs.Save();
        }
    }

    public static int PayingType
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.PAYING_TYPE, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.PAYING_TYPE, value);
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

    

    public static bool CanShowRate
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAN_SHOW_RATE, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAN_SHOW_RATE, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public bool IsTutedReturn
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.IS_TUTED_RETURN, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.IS_TUTED_RETURN, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }


    
    public bool IsTutedBuyStand
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.IS_TUTED_BUY_STAND, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.IS_TUTED_BUY_STAND, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public string CurrentBirdSkin
    {
        get
        {
            return PlayerPrefs.GetString(StringHelper.CURRENT_BIRD_SKIN, "");
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.CURRENT_BIRD_SKIN, value);
            PlayerPrefs.Save();
        }
    }

    public string OwnedBirdSkin
    {
        get
        {
            return PlayerPrefs.GetString(StringHelper.OWNED_BIRD_SKIN, "");
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.OWNED_BIRD_SKIN, value);
            PlayerPrefs.Save();
        }
    }
    public string RandomBirdSkinInShop
    {
        get
        {
            return PlayerPrefs.GetString(StringHelper.RANDOM_BIRD_SKIN_IN_SHOP, "");
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.RANDOM_BIRD_SKIN_IN_SHOP, value);
            PlayerPrefs.Save();
        }
    }
    public string RandomBirdSkinSaleWeekend1
    {
        get
        {
            return PlayerPrefs.GetString(StringHelper.RANDOM_BIRD_SKIN_SALE_WEEKEND_1, "");
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.RANDOM_BIRD_SKIN_SALE_WEEKEND_1, value);
            PlayerPrefs.Save();
        }
    }
    public string RandomBranchSaleWeekend2
    {
        get
        {
            return PlayerPrefs.GetString(StringHelper.RANDOM_BRANCH_SALE_WEEKEND_2, "");
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.RANDOM_BRANCH_SALE_WEEKEND_2, value);
            PlayerPrefs.Save();
        }
    }
    public string RandomThemeSaleWeekend2
    {
        get
        {
            return PlayerPrefs.GetString(StringHelper.RANDOM_THEME_SALE_WEEKEND_2, "");
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.RANDOM_THEME_SALE_WEEKEND_2, value);
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
    public string RandomBranchInShop
    {
        get
        {
            return PlayerPrefs.GetString(StringHelper.RANDOM_BRANCH_IN_SHOP, "");
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.RANDOM_BRANCH_IN_SHOP, value);
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

    public string RandomThemeInShop
    {
        get
        {
            return PlayerPrefs.GetString(StringHelper.RANDOM_THEME_IN_SHOP, "");
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.RANDOM_THEME_IN_SHOP, value);
            PlayerPrefs.Save();
        }
    }

    public string CurrentRandomBird
    {
        get
        {
            return PlayerPrefs.GetString(StringHelper.CURRENT_RANDOM_BIRD_SKIN, "");
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.CURRENT_RANDOM_BIRD_SKIN, value);
            PlayerPrefs.Save();
        }
    }
    public int CurrentRandomBranch
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CURRENT_RANDOM_BRANCH_SKIN, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_RANDOM_BRANCH_SKIN, value);
            PlayerPrefs.Save();
        }
    }
    public int CurrentRandomTheme
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CURRENT_RANDOM_THEME, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_RANDOM_THEME, value);
            PlayerPrefs.Save();
        }
    }

    public int NumShowedAccumulationRewardRandom//Khi có chim mới => bản mới sẽ NumShowedAccumulationRewardRandom = 0
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.NUM_SHOWED_ACCUMULATION_REWARD_RANDOM, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUM_SHOWED_ACCUMULATION_REWARD_RANDOM, value);
            PlayerPrefs.Save();
        }
    }

    public static bool StarterPackIsCompleted
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.STARTER_PACK_IS_COMPLETED, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.STARTER_PACK_IS_COMPLETED, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool HasPackInWeekToday
    {
         get
        {
            return PlayerPrefs.GetInt(StringHelper.HAS_PACK_IN_WEEK_TODAY, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.HAS_PACK_IN_WEEK_TODAY, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public static bool HasPackWeekendToday
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.HAS_PACK_WEEKEND_TODAY, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.HAS_PACK_WEEKEND_TODAY, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public static string CurrentPackInWeek
    {
        get
        {
            return PlayerPrefs.GetString(StringHelper.CURRENT_PACK_IN_WEEK, "");
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.CURRENT_PACK_IN_WEEK, value);
            PlayerPrefs.Save();
        }
    }
    public static string CurrentPackWeekend
    {
        get
        {
            return PlayerPrefs.GetString(StringHelper.CURRENT_PACK_WEEKEND, "");
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.CURRENT_PACK_WEEKEND, value);
            PlayerPrefs.Save();
        }
    }
    public static int NumberOfAdsInPlay;
    public static int NumberOfAdsInDay
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.NUMBER_OF_ADS_IN_DAY, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMBER_OF_ADS_IN_DAY, value);
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

    public static int Lives
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.LIVES_SAVEKEY, 5);
        }
        set
        {
            PlayerPrefs.GetInt(StringHelper.LIVES_SAVEKEY, value);
            PlayerPrefs.Save();
        }
    }
    
}

