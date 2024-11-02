using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HighlightPlus;
using System;

public class WaresController : MonoBehaviour
{
    public ShelfController shelfController;
    
    public Tweener dotwAnim;
    //public HighlightEffect highlightEffect;

    public int waresID;

    public int colID;
    
    public int deptID;

    public Material hideMat;
    public Material showMat;
    public int ColID
    {
        get
        {
            return colID;
        }

        private set
        {
            colID = value;
        }
    }

    public int DeptID
    {
        get
        {
            return deptID;
        }

        private set
        {
            deptID = value;
        }
    }

    public Material[] material;

    public SpriteRenderer SpriteRenderer;

    private Transform modelTrans;

    private int oldIndex;
    private HighlightEffect highlightEffect;

    public enum STATE
    {
        IDLE,
        PROCESS
    }

    public STATE currentState;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitWares()
    {
        showMat = Resources.Load<Material>("Material/ShaderGraphs_SpriteShadow");
        hideMat = Resources.Load<Material>("Material/Default");
        if (transform.childCount > 0)
        {
            modelTrans = transform.GetChild(0);
            //material = modelTrans.GetComponent<MeshRenderer>().materials;
            SpriteRenderer = modelTrans.GetComponent<SpriteRenderer>();
            //modelTrans.gameObject.AddComponent<HighlightEffect>();
            highlightEffect = modelTrans.GetComponent<HighlightEffect>();
        }
        else
        {
            //material = GetComponent<MeshRenderer>().materials;
            SpriteRenderer = modelTrans.GetComponent<SpriteRenderer>();
            //gameObject.AddComponent<HighlightEffect>();
            highlightEffect = GetComponent<HighlightEffect>();
        }

        //highlightEffect.outlineColor = Color.white;
        //highlightEffect.outlineWidth = 0.5f;
        //highlightEffect.highlighted = false;
        currentState = STATE.IDLE;
    }

    public void SetColorHide()
    {
        SpriteRenderer.material = hideMat;
        SpriteRenderer.color = new Color(0.35f, 0.35f, 0.35f, 1.0f);
    }

    public void SetVisible(bool visible)
    {
        SpriteRenderer.enabled = visible;
    }

    public void SetColorShow()
    {
        SpriteRenderer.material = showMat;
        SpriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }


    public void SelectWares()
    {
        currentState = STATE.PROCESS;
        //DoItemShake();
        //clear old slot
        //highlightEffect.enabled = true;
        //highlightEffect.highlighted = true;
        if (highlightEffect == null)
        {
            highlightEffect = modelTrans.GetComponent<HighlightEffect>();
            if (highlightEffect == null)
            {
                modelTrans.gameObject.AddComponent<HighlightEffect>();
                highlightEffect = modelTrans.GetComponent<HighlightEffect>();
            }
            highlightEffect.enabled = true;
            highlightEffect.highlighted = true;
            
            Debug.Log("HightLightNull");
        }
        Debug.Log("Set HigtLight " + highlightEffect.highlighted + "Enabled"+ highlightEffect.enabled);
        oldIndex = shelfController.shelfSlotList[0].waresInRowSlot.IndexOf(this);
        shelfController.shelfSlotList[0].waresInRowSlot[oldIndex] = null;
    }

    public void UnSelectWares()
    {
        DoItemShake();
        //clear old slot
        //highlightEffect.highlighted = false;
        Debug.Log("Off HightLight" + highlightEffect.highlighted);
        shelfController.shelfSlotList[0].waresInRowSlot[oldIndex] = this;
    }

    public void MoveBack()
    {
        //transform.localPosition = FindPosInShelf();
        transform.DOLocalMove(FindPosInShelf(), 0.2f).SetEase(Ease.Linear).SetDelay(0.0f).OnComplete(() =>
        {
            highlightEffect.highlighted = false;
            currentState = STATE.IDLE;
        });

    }

    private Vector3 FindPosInShelf()
    {
        Vector3 pos = Vector3.zero;
        pos = new Vector3((2 * colID + 1) * 0.5f * GameManager.Instance.gamePlaySetting.tileSizeX, 0.0f, -deptID * GameManager.Instance.gamePlaySetting.tileSizeZ);
        return pos;
    }

    public void MoveToAnotherShelf(ShelfController shelf, int emptySlot)
    {
        //bring to new shelf slot
        transform.SetParent(shelf.container.transform);
        shelf.shelfSlotList[0].waresInRowSlot[emptySlot] = this;
        //new coordinate
        colID = emptySlot;
        deptID = 0;
        //start to moving to new position
        shelfController = shelf;
        transform.DOMove(shelfController.shelfSlotList[0].warePointList[emptySlot].position, 0.1f).OnComplete(() =>
        {
            DoItemShake();
        }
        );
        highlightEffect.highlighted = false;
    }

    public void RemoveItem(Action act = null)
    {
        DoItemShake(()=>
        {
            act?.Invoke();
            Destroy(gameObject);
        });

       
    }
    public void DoItemShake(Action act = null)
    {
        if (dotwAnim != null)
        {
            DOTween.Kill(dotwAnim);
        }
        dotwAnim = transform.DOPunchScale(new Vector3(0.32f, -0.1f, 0.2f), 0.3f, 1, 0).OnComplete(() =>
        {
            transform.DOScale(Vector3.one, 0.1f);
            act?.Invoke();
        });
    }
    
}
