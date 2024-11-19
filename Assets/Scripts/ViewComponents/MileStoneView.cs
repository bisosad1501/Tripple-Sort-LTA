using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class MileStoneView : MonoBehaviour
{
    public List<Image> rewardIcon;

    public Text levelUnlockTxt;

    public Text titleTxt;

    public Image progressBar;

    public Color lockColor;

    public Color unLockColor;

    public MileStoneItem currentItem;

    public GameObject gotObj;


    public void ShowView(MileStoneItem item)
    {
        currentItem = item;
        levelUnlockTxt.text = "LV." + item.levelUnlock.ToString();
        titleTxt.text = item.name;
        progressBar.fillAmount = (float)GameManager.Instance.currentLevel / (float)item.levelUnlock;

        if (GameManager.Instance.currentLevel < item.levelUnlock)
        {
            foreach (var i in rewardIcon)
            {
                i.color = lockColor;
            }
        }
        else
        {
            foreach (var i in rewardIcon)
            {
                i.color = unLockColor;
            }
        }
            
        if (currentItem.gotReward == 1)
            gotObj.SetActive(true);

    }
    [Button]
    public void Unlock()
    {
        if (currentItem.gotReward == 0 && currentItem.levelUnlock <= GameManager.Instance.currentLevel)
        {
            AudioManager.instance.btnSound.Play();
            currentItem.gotReward = 1;
            PlayerPrefs.SetInt("GotReward" + currentItem.mileStoneID.ToString(), 1);
            ShowView(currentItem);
            //show reward
            GameManager.Instance.uiManager.rewardAchievementView.InitView(GameManager.Instance.gamePlaySetting.openChestReward);
            GameManager.Instance.uiManager.rewardAchievementView.ShowView();
        }
        else if(currentItem.gotReward == 0)
        {
            AudioManager.instance.btnSound.Play();
            string noti = "Unlock at level " + currentItem.levelUnlock;
        }
       

    }
}
