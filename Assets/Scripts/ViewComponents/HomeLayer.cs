using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class HomeLayer : SliderNode
{
    public Text openChestProgressTxt;

    public Image openChestProgressBar;

   
    public override void InitView()
    {
        openChestProgressTxt.text = GameManager.Instance.currentStar + "/" + GameManager.Instance.gamePlaySetting.openChestNumber;
        

        openChestProgressBar.fillAmount = (float)(GameManager.Instance.currentStar) / (float)(GameManager.Instance.gamePlaySetting.openChestNumber);
    }
       

    public void PlayGame(bool tut = false)
    {
        if (UseProfile.CurrentLevel >= 200)
        {
            GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp
            (
                Input.mousePosition,
                "Coming Soon!!",
                Color.white,
                isSpawnItemPlayer: true
            );
            return;
        }
       
            Debug.Log("PLAY GAME");
            GameManager.Instance.uiManager.gameLoadingView.ShowView();
            GameManager.Instance.InitGame(tut);
    }
    [Button]
    public void OpenChest()
    {
        AudioManager.instance.btnSound.Play();
            if (GameManager.Instance.currentStar >= GameManager.Instance.gamePlaySetting.openChestNumber)
            {
                GameManager.Instance.currentStar -= GameManager.Instance.gamePlaySetting.openChestNumber;
                GameManager.Instance.SaveStar();
                GameManager.Instance.uiManager.homeView.UpdateStarTxt();

                openChestProgressTxt.text = GameManager.Instance.currentStar + "/" + GameManager.Instance.gamePlaySetting.openChestNumber;
                openChestProgressBar.fillAmount = (float)(GameManager.Instance.currentStar) / (float)(GameManager.Instance.gamePlaySetting.openChestNumber);

                GameManager.Instance.uiManager.rewardAchievementView.InitView(GameManager.Instance.gamePlaySetting.openChestReward);
                GameManager.Instance.uiManager.rewardAchievementView.ShowView();
                AudioManager.instance.openGiftSound.Play();
            }
           
    }
}
