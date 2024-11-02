using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

public class FitView : MonoBehaviour
{
    private SpriteRenderer spr;
    private GameObject childObj;
    private BoxCollider boxColl;
    private BoxCollider2D _boxCollider2D;
    private BoxCollider2D[] boxList;
    public Material mat;
    [Button]
    public void CalculateAndFix()
    {
        childObj = transform.GetChild(0).gameObject;
        spr = childObj.GetComponent<SpriteRenderer>();
        boxColl = GetComponent<BoxCollider>();
        _boxCollider2D = childObj.AddComponent<BoxCollider2D>();
        float tmpScl = _boxCollider2D.size.x / _boxCollider2D.size.y;
        float tmpSclB = boxColl.size.x / boxColl.size.y;
        spr.drawMode = SpriteDrawMode.Sliced;
        childObj.transform.localScale = Vector3.one;
        if (tmpScl >= tmpSclB)
        {
            Debug.Log("Tinhs theo X");
            
            spr.size = new Vector2(boxColl.size.x, boxColl.size.x/ tmpScl);
            childObj.transform.localPosition = new Vector3(boxColl.center.x, boxColl.center.y - boxColl.size.y/2f , 0f);
            Debug.Log("(" + boxColl.center.y + " " + boxColl.size.y);
        }
        else
        {
            Debug.Log("Tinhs tyheo Y");
            spr.size = new Vector2(boxColl.size.y * tmpScl, boxColl.size.y);
            childObj.transform.localPosition = new Vector3(boxColl.center.x, boxColl.center.y - boxColl.size.y/2f, 0f);   
            Debug.Log("(" + boxColl.center.y + " " + boxColl.size.y);
        }
        DestroyImmediate(_boxCollider2D, true);
    }

    [Button]
    public void RemoveBox()
    {
        
        boxList = childObj.GetComponents<BoxCollider2D>();
        foreach (var i in boxList)
        {
            DestroyImmediate(i, true);
        }
    }

    [Button]
    public void flip()
    {
        childObj = transform.GetChild(0).gameObject;
        spr = childObj.GetComponent<SpriteRenderer>();
        spr.flipX = false;
    }
    [Button]
    public void ChangeSpriteAndMaterial()
    {
        int name = int.Parse(gameObject.name) % 10000 + 1;
        Sprite spri = Resources.Load<Sprite>("TestSprite/AS_" +name.ToString());
        Debug.Log("Path : TestSprite/AS_" +name.ToString());
        childObj = transform.GetChild(0).gameObject;
        spr = childObj.GetComponent<SpriteRenderer>();
        spr.sprite = spri;
        spr.material = mat;
        spr.shadowCastingMode = ShadowCastingMode.On;
        CalculateAndFix();
    }

    [Button]
    public void AddHighlight()
    {
        childObj = transform.GetChild(0).gameObject;
        if(childObj.GetComponent<HighlightEffect>() == null) childObj.AddComponent<HighlightEffect>();
    }
}
