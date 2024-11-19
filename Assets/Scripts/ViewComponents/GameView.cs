using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using Random = System.Random;

public class GameView : BaseView
{
    [HideInInspector]
    public int currentCollectWares;

    [HideInInspector]
    public int collectWaresMax;

    [HideInInspector]
    public int collectStars;

    [HideInInspector]
    public int comboCount;

    [HideInInspector]
    public int timerMax;
    
    public float currentTime;
    public float timePlay;

    public Text stageProgressTxt;

    public Image stageProgressBar;

    public Image comboLoading;

    public Text currentLevelTxt;

    public Text collectStarTxt;

    public Text timerTxt;

    public Text collectedStarTxt;

    public Text comboTxt;

    public Text currentRankTxt;

    public Text nextRankTxt;

    public Image rankProcessImg;

    public CanvasGroup comboTransform;
    
    
    public enum ComboState
    {
        HIDE,
        SHOWING,
        MOVING_FORWARD,
        MOVING_BACK
    };

    public ComboState currentComboState;

    private Tween comboTween;

    public BoostItem hintItem;

    public BoostItem shuffleItem;

    public BoostItem freezeItem;

    private bool InFreeze;

    public GameObject frozenPanel;

    public TutView tutObj;

    [Header("Chest")]
    public int chestNum;
    public int chestRewardTotal;
    public int currentChest;
    public List<GameObject> ChestIcon;
    public override void InitView()
    {
        if (GameManager.Instance.currentLevel == 1)
        {
            StartCoroutine(ShowTut());
            //StartCoroutine(HideTutIE());
        }
        else
            tutObj.gameObject.SetActive(false);
        
        comboTransform.alpha = 0.0f;
        currentCollectWares = 0;
        GameManager.Instance.inGameStar = 0;
        collectWaresMax = GameManager.Instance.gameBoard.currentLv.totalProductCount * 3;

        stageProgressTxt.text = currentCollectWares.ToString() + "/" + collectWaresMax.ToString();
        stageProgressBar.rectTransform.sizeDelta = new Vector2(140.0f * (float)currentCollectWares / (float)collectWaresMax, 16.0f);
        currentLevelTxt.text = "LV " + GameManager.Instance.currentLevel;

        collectStars = 0;
        collectStarTxt.text = collectStars.ToString();

        timerMax = GameManager.Instance.gameBoard.currentLv.time;

        if (timerMax == -1)
            timerMax = 200;

        currentTime = timerMax;
        timePlay = 0f;
        timerTxt.gameObject.SetActive(true);

        collectedStarTxt.text = GameManager.Instance.inGameStar.ToString();

        comboCount = 0;
        currentComboState = ComboState.HIDE;
        InFreeze = false;
        frozenPanel.SetActive(false);

        hintItem.SetLock();
        shuffleItem.SetLock();
        freezeItem.SetLock();

        if (GameManager.Instance.currentLevel >= 2)
        {
            if (GameManager.Instance.currentHint > 0)
                hintItem.SetAvailable(GameManager.Instance.currentHint);
            else
                hintItem.SetUnAvailable();
        }

        if (GameManager.Instance.currentLevel >= 3)
        {
            if (GameManager.Instance.currentShuffle > 0)
                shuffleItem.SetAvailable(GameManager.Instance.currentShuffle);
            else
                shuffleItem.SetUnAvailable();
        }

        if (GameManager.Instance.currentLevel >= 4)
        {
            if (GameManager.Instance.currentFreeze > 0)
                freezeItem.SetAvailable(GameManager.Instance.currentFreeze);
            else
                freezeItem.SetUnAvailable();
        }
        InitChestBar();
        UpdateRankBar();
    }

    private void InitChestBar()
    {
        if (Rand() <= 80)
        {
            chestNum = 3;
        }
        else chestNum = 2;

        foreach (var i in ChestIcon)
        {
            i.SetActive(false);
            i.transform.GetChild(0).gameObject.SetActive(false);
            i.GetComponent<Image>().color = Color.white;
        }
        if (collectWaresMax > 45)
        {
            chestRewardTotal = 45;
        }
        else chestRewardTotal = collectWaresMax;

        int gap = 300 / chestNum;
        
        for (int i = 0; i < chestNum; i++)
        {
            ChestIcon[i].GetComponent<RectTransform>().anchoredPosition = new Vector2((i + 1) * gap - 150f, 0);
            ChestIcon[i].SetActive(true);
        }
        currentChest = 1;
    }
    private int Rand()
    {
        return UnityEngine.Random.Range(1, 101);
    }
    public void DoShakeChest(int index, Action act = null)
    {
        DOTween.Kill(ChestIcon[index].transform);
        ChestIcon[index].transform.localScale = Vector3.one;
        ChestIcon[index].transform.DOPunchScale(new Vector3(0.32f, -0.1f, 0.2f), 0.5f, 1, 0).OnComplete(() =>
        {
            act?.Invoke();
        });
    }
    public void UpdateRankBar()
    {
        if (currentChest <= chestNum)
        {
            DoShakeChest(currentChest - 1);
            rankProcessImg.DOFillAmount((currentCollectWares)
                                        / (float)(chestRewardTotal), 0.5f);
            if ((float)currentCollectWares >= (float)chestRewardTotal * (float)currentChest / (float)chestNum)
            {
                //GameManager.Instance.uiManager.rwRankView.ShowView();
                ChestIcon[currentChest - 1].transform.GetChild(0).gameObject.SetActive(true);
                ChestIcon[currentChest - 1].GetComponent<Image>().color = Color.grey;
                currentChest++;
            }
        }
    }
    
    public void RefreshBoostValue()
    {
        if (GameManager.Instance.currentHint > 0)
            hintItem.SetAvailable(GameManager.Instance.currentHint);
        else
            hintItem.SetUnAvailable();

        if (GameManager.Instance.currentShuffle > 0 && GameManager.Instance.currentLevel >= 3)
            shuffleItem.SetAvailable(GameManager.Instance.currentShuffle);
        else if(GameManager.Instance.currentLevel >= 3)
            shuffleItem.SetUnAvailable();
        else 
            shuffleItem.SetLock();

        if (GameManager.Instance.currentFreeze > 0 && GameManager.Instance.currentLevel >= 4)
            freezeItem.SetAvailable(GameManager.Instance.currentFreeze);
        else if(GameManager.Instance.currentLevel >= 4)
            freezeItem.SetUnAvailable();
        else
            freezeItem.SetLock();
    }

    public override void Start()
    {

    }

    IEnumerator ShowTut()
    {
        yield return new WaitForSeconds(1.0f);
        tutObj.gameObject.SetActive(true);
    }

    IEnumerator HideTutIE()
    {
        yield return new WaitForSeconds(6.0f);
        tutObj.gameObject.SetActive(false);
    }

    public void HideTut()
    {
        tutObj.gameObject.SetActive(false);
    }

    public override void Update()
    {

        if (GameManager.Instance.currentState == GameManager.GAME_STATE.IN_GAME && !InFreeze)
            UpdateTimer();
    }

    public void UpdateStageProgress()
    {
        currentCollectWares += 3;

        int randomUnlockStamp = 0;

         randomUnlockStamp = UnityEngine.Random.Range(0, 20);
        if (randomUnlockStamp == 10)
            GameManager.Instance.UnlockStamp();

        GameManager.Instance.currentXPRank++;
        UpdateRankBar();
        stageProgressTxt.text = currentCollectWares.ToString() + "/" + collectWaresMax.ToString();
        stageProgressBar.rectTransform.sizeDelta = new Vector2(140.0f * (float)currentCollectWares / (float)collectWaresMax, 16.0f);
        comboCount++;
        if (comboCount >= 2)
        {
            comboTxt.text = "X" + (comboCount - 1).ToString();
            if (currentComboState == ComboState.HIDE)
                ShowCombo();
            else if (currentComboState == ComboState.SHOWING)
            {
                currentComboProgressMax = 100;
                comboTween.Restart();
            }
            else if (currentComboState == ComboState.MOVING_FORWARD)
            {

            }
            else if (currentComboState == ComboState.MOVING_BACK)
            {
                ReturnCombo();
            }
        }

        if (currentCollectWares == collectWaresMax)
        {
            GameManager.Instance.currentState = GameManager.GAME_STATE.LEVEL_CLEAR;
            GameManager.Instance.ShowGameWin();
            Debug.Log("GAME WIN");
        }
    }

    public void GetStarCombo(int combo)
    {
        GameManager.Instance.inGameStar += combo;
        collectStarTxt.text = GameManager.Instance.inGameStar.ToString();
    }

    public void HideViewBack()
    {
        AudioManager.instance.btnSound.Play();
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0.0f, 0.1f).SetEase(Ease.Linear)
            .OnComplete(() => {

                canvasGroup.alpha = 0.0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                isShow = false;
            });
    }
    
    private void UpdateTimer()
    {
        if (GameManager.Instance.currentState == GameManager.GAME_STATE.TIME_OUT)
            return;

        if (GameManager.Instance.currentState == GameManager.GAME_STATE.LEVEL_CLEAR)
            return;
        
        if (currentTime - Time.deltaTime > 0.0f)
        {
            currentTime -= Time.deltaTime;
            timerTxt.text = TimeSpan.FromSeconds(currentTime).ToString(@"mm\:ss");
            timePlay += Time.deltaTime;
        }
        else
        {
            Debug.Log("Time OUT");
            timerTxt.gameObject.SetActive(false);
            GameManager.Instance.currentState = GameManager.GAME_STATE.TIME_OUT;
            timePlay = 0f;
            GameManager.Instance.ShowGameFail();
        }
    }

    public void ResumeTimer()
    {
        GameManager.Instance.currentState = GameManager.GAME_STATE.IN_GAME;
        currentTime = 60;
        timerTxt.gameObject.SetActive(true);
    }

    private float currentComboProgress;

    private float currentComboProgressMax;

    private Vector3 comboPosOriginal;

    private void ShowCombo()
    {

        /*
        currentComboProgressMax = 100;
        currentComboState = ComboState.MOVING_FORWARD;
        comboTransform.DOLocalMoveX(-140, 0.25f).SetRelative(true).SetEase(Ease.Linear).OnComplete
        (
            () =>
            {
                currentComboState = ComboState.SHOWING;
            }
        );

        comboTween = DOTween.To(() => currentComboProgressMax, x => currentComboProgress = x, 0, 7.5f).SetEase(Ease.Linear)
              .OnUpdate(() =>
              {
                  //Debug.Log("current Combo " + currentComboProgress);
                  comboLoading.fillAmount = currentComboProgress / currentComboProgressMax;

              })
              .OnComplete(() =>
              {
                  HideCombo();
              });
        */

        currentComboState = ComboState.SHOWING;
        currentComboProgressMax = 100;
        comboTransform.alpha = 1.0f;
        comboTween = DOTween.To(() => currentComboProgressMax, x => currentComboProgress = x, 0, 7.5f).SetEase(Ease.Linear)
             .OnUpdate(() =>
             {
                 //Debug.Log("current Combo " + currentComboProgress);
                 comboLoading.fillAmount = currentComboProgress / currentComboProgressMax;

             })
             .OnComplete(() =>
             {
                 HideCombo();
             });
    }

    private void HideCombo()
    {
        /*
        currentComboState = ComboState.MOVING_BACK;
        comboTransform.DOLocalMoveX(140, 0.25f).SetRelative(true).SetEase(Ease.Linear).OnComplete
        (
            () =>
            {
                currentComboState = ComboState.HIDE;
                comboCount = 0;
            }
        );
        */
        currentComboState = ComboState.HIDE;
        comboCount = 0;
        comboTransform.alpha = 0.0f;
    }

    private void ReturnCombo()
    {
        /*
        currentComboState = ComboState.MOVING_FORWARD;
        float distance = 140.0f - comboTransform.localPosition.x;
        comboTransform.DOLocalMoveX(distance, 0.25f * distance / 140.0f).SetRelative(true).SetEase(Ease.Linear).OnComplete
        (
            () =>
            {
                currentComboState = ComboState.SHOWING;
            }
        );
        */
        comboTransform.alpha = 1.0f;
    }
    
    public void UseHint()
    {
        AudioManager.instance.btnSound.Play();
        if (hintItem.currentState == BoostItem.STATE.AVAILABLE)
        {
                Debug.Log("Use Hint");
                GameManager.Instance.gameBoard.ProcessHint();
        }
        else if (hintItem.currentState == BoostItem.STATE.UNAVAILABLE)
        {
            GameManager.Instance.uiManager.buyBoostView.InitView(BuyBoostView.BoostType.HINT);
            GameManager.Instance.uiManager.buyBoostView.ShowView();
        }

    }

    public void UseHIntComplete()
    {
        GameManager.Instance.currentHint--;
        UseProfile.Hint =  GameManager.Instance.currentHint;
        hintItem.ShowCountText(GameManager.Instance.currentHint);
        if (GameManager.Instance.currentHint == 0)
            hintItem.SetUnAvailable();
    }

    public void UseShuffle()
    {
        AudioManager.instance.btnSound.Play();
        if (shuffleItem.currentState == BoostItem.STATE.AVAILABLE)
        {
            Debug.Log("Use Shuffle");
            GameManager.Instance.gameBoard.ProcessShuffle();

            GameManager.Instance.currentShuffle--;
            PlayerPrefs.SetInt("Shuffle", GameManager.Instance.currentShuffle);
            shuffleItem.ShowCountText(GameManager.Instance.currentShuffle);
            if (GameManager.Instance.currentShuffle == 0)
                shuffleItem.SetUnAvailable();
        }
        else if (shuffleItem.currentState == BoostItem.STATE.UNAVAILABLE)
        {
            GameManager.Instance.uiManager.buyBoostView.InitView(BuyBoostView.BoostType.SHUFFLE);
            GameManager.Instance.uiManager.buyBoostView.ShowView();
        }

    }

    public void UseFreeze()
    {
        if(InFreeze) return;
        AudioManager.instance.btnSound.Play();
        if (freezeItem.currentState == BoostItem.STATE.AVAILABLE)
        {
            //Debug.Log("Use Freeze");
            InFreeze = true;
            StartCoroutine(DisableFreeze());

            GameManager.Instance.currentFreeze--;
            UseProfile.Freeze = GameManager.Instance.currentFreeze;
            freezeItem.ShowCountText(GameManager.Instance.currentFreeze);
            if (GameManager.Instance.currentFreeze == 0)
                freezeItem.SetUnAvailable();
        }

        else if (freezeItem.currentState == BoostItem.STATE.UNAVAILABLE)
        {
            GameManager.Instance.uiManager.buyBoostView.InitView(BuyBoostView.BoostType.FREEZE);
            GameManager.Instance.uiManager.buyBoostView.ShowView();
        }
    }

    IEnumerator DisableFreeze()
    {
        frozenPanel.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        InFreeze = false;
        frozenPanel.SetActive(false);
    }

    public void PauseGame()
    {
        AudioManager.instance.btnSound.Play();
        GameManager.Instance.uiManager.pauseView.ShowView();
    }

    public void RemoveAds()
    {
        AudioManager.instance.btnSound.Play();
    }
}
