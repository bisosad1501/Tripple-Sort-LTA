using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class BuyBoostView : BaseView
{
    public enum BoostType
    {
        HINT,
        SHUFFLE,
        FREEZE
    }

    public BoostType currentType;

    public Image titleIcon;

    public Image freePack1Icon;

    public Image freePack2Icon;

    public List<BuyPackageData> IAPPackageData = new List<BuyPackageData>();

    public List<BuyPackageView> IAPPackageView = new List<BuyPackageView>();

    public Sprite[] boostIconList;

    public override void InitView()
    {
    }

    public void InitView(BoostType type)
    {
        currentType = type;
    }

    public override void ShowView()
    {
        base.ShowView();
        GameManager.Instance.currentState = GameManager.GAME_STATE.IN_GAME_POPUP;
        Sprite itemSpr = null;

        if (currentType == BoostType.HINT)
            itemSpr = boostIconList[0];
        else if (currentType == BoostType.SHUFFLE)
            itemSpr = boostIconList[1];
        else if (currentType == BoostType.FREEZE)
            itemSpr = boostIconList[2];

        titleIcon.sprite = itemSpr;
        titleIcon.SetNativeSize();
        freePack1Icon.sprite = itemSpr;
        freePack1Icon.SetNativeSize();
        freePack2Icon.sprite = itemSpr;
        freePack2Icon.SetNativeSize();

        for (int i = 0; i < IAPPackageData.Count; i++)
        {
            IAPPackageView[i].itemIcon.sprite = itemSpr;
            IAPPackageView[i].itemIcon.SetNativeSize();
            IAPPackageView[i].priceTxt.text = "$" + IAPPackageData[i].priceValue;
            IAPPackageView[i].totalTxt.text = "x" + IAPPackageData[i].totalValue;
        }
    }

    public override void Start()
    {
    }

    public override void Update()
    {
    }

    public void BuyByCoin()
    {
        AudioManager.instance.btnSound.Play();
        if (GameManager.Instance.currentCoin >= 400)
        {
            GameManager.Instance.AddCoin(-400);
            if (currentType == BoostType.HINT)
                GameManager.Instance.AddHint(1);
            else if (currentType == BoostType.SHUFFLE)
                GameManager.Instance.AddShuffle(1);
            else if (currentType == BoostType.FREEZE)
                GameManager.Instance.AddFreeze(1);
            HideView();
        }
        else
        {
            HideView();
            GameManager.Instance.uiManager.storeView.ShowView();
        }
    }


    public void MoreCoins()
    {
        AudioManager.instance.btnSound.Play();
        HideView();
    }


}


[System.Serializable]
public class BuyPackageView
{
    public Image itemIcon;

    public Text totalTxt;

    public Text priceTxt;
}


[System.Serializable]
public class BuyPackageData
{
    public int totalValue;

    public float priceValue;
}