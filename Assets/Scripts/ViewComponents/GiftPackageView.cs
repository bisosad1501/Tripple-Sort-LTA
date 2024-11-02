using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftPackageView : MonoBehaviour
{
    public Text packageName;

    public Image packageIcon;

    public Text packagePriceText;

    public List<GiftView> giftList;

    public Sprite[] itemSprList;

    public Sprite[] packageSprList;
    public Sprite[] backGroundSprList;

    public Image backGround;

    [HideInInspector]
    public GiftPackageItem currentItem;

   
    protected Action<object> actionIAPInited;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowView(GiftPackageItem item)
    {
        currentItem = item;
        packageName.text = item.packageName;
        packageIcon.sprite = packageSprList[item.packID];
        backGround.sprite = backGroundSprList[item.packID];
        for (int i = 0; i < giftList.Count; i++)
        {
            giftList[i].giftIcon.sprite = itemSprList[i];
            giftList[i].valueText.text = item.itemValueList[i].ToString();
        }
    }

    public void Purchase()
    {
    }
}

[System.Serializable]
public class GiftView
{
    public Image giftIcon;

    public Text valueText;
}