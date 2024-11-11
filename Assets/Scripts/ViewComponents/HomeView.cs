using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeView : BaseView
{
    public SliderView sliderView;

    public Text coinTxt;
    public Text starTxt;
    public Text namePlayer;

    public override void InitView()
    {
        sliderView.InitView();
        UpdateCoinTxt();
        UpdateStarTxt();
        namePlayer.text = UseProfile.NamePlayer;
    }

    public void UpdateCoinTxt()
    {
        coinTxt.text = GameManager.Instance.currentCoin.ToString();
    }

    public void UpdateStarTxt()
    {
        starTxt.text = GameManager.Instance.currentStar.ToString();
    }

    public override void Start()
    {
        // Initialization logic can go here
    }

    public override void Update()
    {
        
    }

    public override void ShowView()
    {
        base.ShowView();
        sliderView.ShowMidleNode();
    }

    public override void HideView()
    {
        base.HideView();
        AudioManager.instance.btnSound.Play();
    }

    public void ShowSetting()
    {
        AudioManager.instance.btnSound.Play();
        GameManager.Instance.uiManager.settingView.ShowView();
    }

    public void ShowShop()
    {
        AudioManager.instance.btnSound.Play();
        sliderView.ShowNodeByIndex(0);
    }

    public void ShowLogin()
    {
        AudioManager.instance.btnSound.Play();
        GameManager.Instance.uiManager.loginView.ShowView();
    }
}