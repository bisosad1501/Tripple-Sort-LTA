using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPackageView : MonoBehaviour
{
    public Text valueTxt;

    public Text priceTxt;

    public Image packageIcon;

    public Sprite[] iconList;

    [HideInInspector]
    public CoinPackageItem currentItem;
        
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowView(CoinPackageItem item)
    {
        currentItem = item;
        valueTxt.text = item.packageValue;
        packageIcon.sprite = iconList[item.packID];

    }

    public void Purchase()
    {
     
    }
}
