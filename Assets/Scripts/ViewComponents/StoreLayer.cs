using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.Advertisements;

public class StoreLayer : SliderNode
{
    //gift item
    public List<GiftPackageItem> giftPackageList;

    public GiftPackageView giftPackageViewPrefab;

    public RectTransform giftPackageViewRoot;

    //coin item
    public List<CoinPackageItem> coinPackageList;

    public CoinPackageView coinPackageViewPrefab;

    public RectTransform coinPackageViewRoot;

    //life item
    public List<LifePackageItem> lifePackageList;

    public LifePackageView lifePackageViewPrefab;

    public RectTransform lifePackageViewRoot;



    [SerializeField] private Button freeAdCoin;
    [SerializeField] private Button freeAdLive;
    protected Action<object> actionIAPInited;

    public override void InitView()
    {

    }

    private void LoadItem()
    {
        for (int i = 0; i < giftPackageList.Count; i++)
        {
            GiftPackageView itemView = Instantiate(giftPackageViewPrefab);
            itemView.transform.parent = giftPackageViewRoot;
            itemView.transform.localScale = Vector3.one;
            itemView.ShowView(giftPackageList[i]);
        }

        for (int i = 0; i < coinPackageList.Count; i++)
        {
            CoinPackageView itemView = Instantiate(coinPackageViewPrefab);
            itemView.transform.parent = coinPackageViewRoot;
            itemView.transform.localScale = Vector3.one;
            itemView.ShowView(coinPackageList[i]);
        }

        for (int i = 0; i < lifePackageList.Count; i++)
        {
            LifePackageView itemView = Instantiate(lifePackageViewPrefab);
            itemView.transform.parent = lifePackageViewRoot;
            itemView.transform.localScale = Vector3.one;
            itemView.ShowView(lifePackageList[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadItem();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FreeCoin()
    {
        AudioManager.instance.btnSound.Play();
        freeAdCoin.interactable = false;

        //GameManager.Instance.freeAdsTimer.ConsumeLife();
        GameController.Instance.moneyEffectController.SpawnEffect_FlyUp(
            new Vector3(Input.mousePosition.x / 2f, Input.mousePosition.y / 2f, Input.mousePosition.z), GiftType.Coin,
            10, Color.white, isSpawnItemPlayer: true);
        GameController.Instance.moneyEffectController.SpawnEffect_FlyUp(
            new Vector3(Input.mousePosition.x / 2f, Input.mousePosition.y / 2f, Input.mousePosition.z), GiftType.Coin,
            10, Color.white, isSpawnItemPlayer: true);
        GameController.Instance.moneyEffectController.SpawnEffect_FlyUp(
            new Vector3(Input.mousePosition.x / 2f, Input.mousePosition.y / 2f, Input.mousePosition.z), GiftType.Coin,
            10, Color.white, isSpawnItemPlayer: true);
        GameManager.Instance.AddCoin(10);
        freeAdCoin.interactable = true;


    }

    public void FreeLive()
    {
        AudioManager.instance.btnSound.Play();
        freeAdLive.interactable = false;
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
            new Vector3(Input.mousePosition.x / 2f, Input.mousePosition.y / 2f, Input.mousePosition.z), GiftType.Heart,
            1, Color.white, isSpawnItemPlayer: true);
        GameController.Instance.moneyEffectController.SpawnEffect_FlyUp(
            new Vector3(Input.mousePosition.x / 2f, Input.mousePosition.y / 2f, Input.mousePosition.z), GiftType.Heart,
            1, Color.white, isSpawnItemPlayer: true);
        GameController.Instance.moneyEffectController.SpawnEffect_FlyUp(
            new Vector3(Input.mousePosition.x / 2f, Input.mousePosition.y / 2f, Input.mousePosition.z), GiftType.Heart,
            1, Color.white, isSpawnItemPlayer: true);
        GameManager.Instance.livesManager.GiveOneLife();
        freeAdLive.interactable = true;
    }

    public void Restore()
    {
        AudioManager.instance.btnSound.Play();
        Debug.Log("Restore");
    }

    public void UpdateFreeAdsTxt()
    {
        GameManager.Instance.uiManager.storeView.UpdateFreeAdsTxt();
    }

    public void UpdateFreeAdsTimerTxt()
    {
        GameManager.Instance.uiManager.storeView.UpdateFreeAdsTimerTxt();
    }

}

[System.Serializable]
public class GiftPackageItem
{
    public string packageName;

    public string packagePrice;

    public int packID;

    public List<int> itemValueList;
    
}

[System.Serializable]
public class CoinPackageItem
{
    public string packageValue;

    public string packagePrice;

    public int packID;

}

[System.Serializable]
public class LifePackageItem
{
    public string packageValue;

    public string packagePrice;

    public int packID;

}
