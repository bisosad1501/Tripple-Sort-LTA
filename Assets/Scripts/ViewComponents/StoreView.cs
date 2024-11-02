using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreView : BaseView
{
    //gift item
    public RectTransform giftPackageViewRoot;

    //coin item

    public RectTransform coinPackageViewRoot;

    //life item
    public RectTransform lifePackageViewRoot;

    public StoreLayer storelayer;

    public Text coinTxt;

    public Text starTxt;

    public Text heartTxt;

    public Text heartFullTxt;

    public Text freeAdsValueTxt;

    public Text freeAdsTimerTxt;
    
    [SerializeField] private Button freeAdCoin; 
    [SerializeField] private Button freeAdLive; 

    public override void InitView()
    {
        UpdateCoinTxt();
        UpdateStarTxt();
    }

    public override void Start()
    {
        LoadItem();
    }

    public override void Update()
    {
        
    }

    private void LoadItem()
    {
        for (int i = 0; i < storelayer.giftPackageList.Count; i++)
        {
            GiftPackageView itemView = Instantiate(storelayer.giftPackageViewPrefab);
            itemView.transform.parent = giftPackageViewRoot;
            itemView.transform.localScale = Vector3.one;
            itemView.ShowView(storelayer.giftPackageList[i]);
        }

        for (int i = 0; i < storelayer.coinPackageList.Count; i++)
        {
            CoinPackageView itemView = Instantiate(storelayer.coinPackageViewPrefab);
            itemView.transform.parent = coinPackageViewRoot;
            itemView.transform.localScale = Vector3.one;
            itemView.ShowView(storelayer.coinPackageList[i]);
        }

        for (int i = 0; i < storelayer.lifePackageList.Count; i++)
        {
            LifePackageView itemView = Instantiate(storelayer.lifePackageViewPrefab);
            itemView.transform.parent = lifePackageViewRoot;
            itemView.transform.localScale = Vector3.one;
            itemView.ShowView(storelayer.lifePackageList[i]);
        }
    }

    public void ClosePopup()
    {
        AudioManager.instance.btnSound.Play();
        HideView();
    }

    public void UpdateCoinTxt()
    {
        coinTxt.text = GameManager.Instance.currentCoin.ToString();
    }

    public void UpdateStarTxt()
    {
        starTxt.text = GameManager.Instance.currentStar.ToString();
    }

    public void OnLivesChanged()
    {
        heartTxt.text = GameManager.Instance.livesManager.LivesText;
    }

    public void OnTimeToNextLifeChanged()
    {
        heartFullTxt.text = GameManager.Instance.livesManager.RemainingTimeString;
    }

    public void UpdateFreeAdsTxt()
    {
        
    }

    public void UpdateFreeAdsTimerTxt()
    {
    }

    public void FreeCoin()
    {
        AudioManager.instance.btnSound.Play();
        freeAdCoin.interactable = false;
       
                //GameManager.Instance.freeAdsTimer.ConsumeLife();
                GameManager.Instance.AddCoin(10);
                GameController.Instance.moneyEffectController.SpawnEffect_FlyUp( new Vector3(Input.mousePosition.x / 2f, Input.mousePosition.y / 2f, Input.mousePosition.z),
                    GiftType.Coin, 10, Color.white, isSpawnItemPlayer: true);
                GameController.Instance.moneyEffectController.SpawnEffect_FlyUp( new Vector3(Input.mousePosition.x / 2f, Input.mousePosition.y / 2f, Input.mousePosition.z),
                    GiftType.Coin, 10, Color.white, isSpawnItemPlayer: true);
                GameController.Instance.moneyEffectController.SpawnEffect_FlyUp( new Vector3(Input.mousePosition.x / 2f, Input.mousePosition.y / 2f, Input.mousePosition.z),
                    GiftType.Coin, 10, Color.white, isSpawnItemPlayer: true);
                freeAdCoin.interactable = true; 
    }

    public void FreeLive()
    {
        freeAdLive.interactable = false;
        AudioManager.instance.btnSound.Play();
        if (GameManager.Instance.livesManager.HasMaxLives)
        {
            GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp
            (
                Input.mousePosition,
                "Can't get more heart",
                Color.white,
                isSpawnItemPlayer: true
            );
            freeAdLive.interactable = true;
            return;
        }

                //GameManager.Instance.freeAdsTimer.ConsumeLife();
                GameController.Instance.moneyEffectController.SpawnEffect_FlyUp(
                    new Vector3(Input.mousePosition.x / 2f, Input.mousePosition.y / 2f, Input.mousePosition.z),
                    GiftType.Heart, 1, Color.white, isSpawnItemPlayer: true);
                GameController.Instance.moneyEffectController.SpawnEffect_FlyUp(
                    new Vector3(Input.mousePosition.x / 2f, Input.mousePosition.y / 2f, Input.mousePosition.z),
                    GiftType.Heart, 1, Color.white, isSpawnItemPlayer: true);
                GameController.Instance.moneyEffectController.SpawnEffect_FlyUp(
                    new Vector3(Input.mousePosition.x / 2f, Input.mousePosition.y / 2f, Input.mousePosition.z),
                    GiftType.Heart, 1, Color.white, isSpawnItemPlayer: true);
                GameManager.Instance.livesManager.GiveOneLife();
                freeAdLive.interactable = true;  

    }

    public override void ShowView()
    {
        base.ShowView();
        GameManager.Instance.currentState = GameManager.GAME_STATE.IN_GAME_POPUP;
    }
    public void Restore()
    {
        AudioManager.instance.btnSound.Play();
        Debug.Log("Restore");
    }

    public void RemoveAds()
    {
        AudioManager.instance.btnSound.Play();
    }
}
