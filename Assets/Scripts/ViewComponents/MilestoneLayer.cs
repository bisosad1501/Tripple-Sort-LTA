using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilestoneLayer : SliderNode
{
    private List<MileStoneView> mileStoneList = new List<MileStoneView>();

    public List<MileStoneItem> itemList = new List<MileStoneItem>();

    public MileStoneView mileStonePrefab;

    private bool isLoadItems;

    public RectTransform root;

    private void Awake()
    {
        isLoadItems = false;
    }

    public override void InitView()
    {
        LoadData();

        if (!isLoadItems)
            LoadItems();
        else
            RefreshItems();
    }

    

    private void LoadItems()
    {
        if (!isLoadItems)
            isLoadItems = true;

        for (int i = 0; i < itemList.Count; i++)
        {
            MileStoneView item = Instantiate(mileStonePrefab);
            item.transform.SetParent(root);
            item.transform.localScale = new Vector3(0.67f, 0.67f, 0.67f);
            item.ShowView(itemList[i]);
            mileStoneList.Add(item);
        }
    }

    private void RefreshItems()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            mileStoneList[i].ShowView(itemList[i]);
        }
    }

    private void LoadData()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].progressValue = GameManager.Instance.currentLevel;
            itemList[i].gotReward = PlayerPrefs.GetInt("GotReward" + i.ToString());
        }
    }

}


[System.Serializable]
public class MileStoneItem
{
    public int mileStoneID;

    public int levelUnlock;

    public int gotReward;

    [HideInInspector]
    public int progressValue;

    public string name;

}
