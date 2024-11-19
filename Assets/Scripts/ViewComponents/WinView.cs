using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static BuyBoostView;
using UnityEngine.Advertisements;

public class WinView : BaseView
{
    public RectTransform topTrans;

    public RectTransform contentTrans;

    public RectTransform victoryBanner;

    public CanvasGroup propBg;

    public CanvasGroup adsMulti;

    public CanvasGroup btnGroup;

    public CanvasGroup nextBtn;

    public RectTransform lightTrans;

    public RectTransform arrowTrans;

    public Text claimTxt;

    public Text gotStarTxt;

    public Text rewardVictoryProgressTxt;

    public Image rewardVictoryProgressImg;

    public Text luckySpinProgressTxt;

    public Image luckySpinProgressImg;

    public int multiAdsCount;

    public Tween moveArrowTween;

    public RectTransform starVfxTarget;

    public GameObject starVfx1;

    public GameObject starVfx2;

    public GameObject starVfx3;

    private Vector3 localPos1;

    private Vector3 localPos2;

    private Vector3 localPos3;

    public Button claimButton;

    public Button nextButton;

    public override void InitView()
    {
        gotStarTxt.text = GameManager.Instance.inGameStar.ToString();
        //PlayerPrefs.SetInt("Star", GameManager.Instance.inGameStar);

        if (GameManager.Instance.currentRewardVictoryProgress <
            GameManager.Instance.gamePlaySetting.rewardVictoryProgressMax)
        {
            GameManager.Instance.currentRewardVictoryProgress++;
        }

        if (GameManager.Instance.currentLuckySpinProgress < GameManager.Instance.gamePlaySetting.luckySpinProgressMax)
        {
            GameManager.Instance.currentLuckySpinProgress++;
        }

        rewardVictoryProgressTxt.text = GameManager.Instance.currentRewardVictoryProgress + "/" +
                                        GameManager.Instance.gamePlaySetting.rewardVictoryProgressMax;
        rewardVictoryProgressImg.fillAmount = (float)GameManager.Instance.currentRewardVictoryProgress /
                                              (float)GameManager.Instance.gamePlaySetting.rewardVictoryProgressMax;

        luckySpinProgressTxt.text = GameManager.Instance.currentLuckySpinProgress + "/" +
                                    GameManager.Instance.gamePlaySetting.luckySpinProgressMax;
        luckySpinProgressImg.fillAmount = (float)GameManager.Instance.currentLuckySpinProgress /
                                          (float)GameManager.Instance.gamePlaySetting.luckySpinProgressMax;

        //reset if it reach max value
        if (GameManager.Instance.currentRewardVictoryProgress ==
            GameManager.Instance.gamePlaySetting.rewardVictoryProgressMax)
        {
            GameManager.Instance.uiManager.rewardVictoryView.InitView();
            GameManager.Instance.uiManager.rewardVictoryView.ShowView();
            GameManager.Instance.currentRewardVictoryProgress = 0;
        }

        if (GameManager.Instance.currentLuckySpinProgress == GameManager.Instance.gamePlaySetting.luckySpinProgressMax)
        {
            GameManager.Instance.uiManager.luckyWheelView.InitView();
            GameManager.Instance.uiManager.luckyWheelView.ShowView();
            GameManager.Instance.currentLuckySpinProgress = 0;
        }

        UseProfile.LuckySpinProgress = GameManager.Instance.currentLuckySpinProgress;
        UseProfile.RewardVictoryProgress = GameManager.Instance.currentRewardVictoryProgress;

        UseProfile.XpRank = GameManager.Instance.currentXPRank;
    }

    public override void ShowView()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        isShow = true;

        contentTrans.gameObject.SetActive(false);
        victoryBanner.localScale = new Vector3(0.5f, 1.0f, 1.0f);
        //arrowTrans.localPosition = new Vector3(-200.0f, 73.0f, 0.0f);
        lightTrans.DOLocalRotate(new Vector3(0.0f, 0.0f, -360.0f), 2.0f).SetRelative().SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);

        propBg.alpha = 0.0f;
        adsMulti.alpha = 0.0f;
        btnGroup.alpha = 0.0f;
        nextBtn.alpha = 0.0f;

        victoryBanner.DOScaleX(1.0f, 0.25f).SetEase(Ease.Linear).OnComplete
        (
            () =>
            {
                contentTrans.gameObject.SetActive(true);
                contentTrans.localScale = new Vector3(1.0f, 0.5f, 1.0f);
                contentTrans.DOScaleY(1.0f, 0.25f).SetEase(Ease.Linear).OnComplete
                (
                    () =>
                    {
                        propBg.DOFade(1.0f, 0.35f).SetEase(Ease.Linear).OnComplete
                        (
                            () =>
                            {
                                adsMulti.DOFade(1.0f, 0.35f).SetEase(Ease.Linear).OnComplete
                                (
                                    () =>
                                    {
                                        if (moveArrowTween == null)
                                            moveArrowTween = arrowTrans.DOLocalMoveX(470f, 1.5f).SetRelative(true)
                                                .SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).OnUpdate
                                                (
                                                    () =>
                                                    {
                                                        if (arrowTrans.localPosition.x >= -235.0f &&
                                                            arrowTrans.localPosition.x <= -176.0f)
                                                        {
                                                            claimTxt.text = "Claim X2";
                                                            multiAdsCount = 2;
                                                        }
                                                        else if (arrowTrans.localPosition.x >= -176.0f &&
                                                                 arrowTrans.localPosition.x <= -111.0f)
                                                        {
                                                            claimTxt.text = "Claim X3";
                                                            multiAdsCount = 3;
                                                        }
                                                        else if (arrowTrans.localPosition.x >= -111.0f &&
                                                                 arrowTrans.localPosition.x <= -50.0f)
                                                        {
                                                            claimTxt.text = "Claim X4";
                                                            multiAdsCount = 4;
                                                        }

                                                        else if (arrowTrans.localPosition.x >= -50.0f &&
                                                                 arrowTrans.localPosition.x <= 50.0f)
                                                        {
                                                            claimTxt.text = "Claim X5";
                                                            multiAdsCount = 5;
                                                        }
                                                        else if (arrowTrans.localPosition.x >= 50.0f &&
                                                                 arrowTrans.localPosition.x <= 111.0f)
                                                        {
                                                            claimTxt.text = "Claim X4";
                                                            multiAdsCount = 4;
                                                        }
                                                        else if (arrowTrans.localPosition.x >= 111.0f &&
                                                                 arrowTrans.localPosition.x <= 176.0f)
                                                        {
                                                            claimTxt.text = "Claim X3";
                                                            multiAdsCount = 4;
                                                        }
                                                        else if (arrowTrans.localPosition.x >= 176.0f &&
                                                                 arrowTrans.localPosition.x <= 235.0f)
                                                        {
                                                            claimTxt.text = "Claim X2";
                                                            multiAdsCount = 2;
                                                        }
                                                    }
                                                );
                                        else moveArrowTween.Play();

                                        btnGroup.DOFade(1.0f, 0.35f).SetEase(Ease.Linear).OnComplete
                                        (
                                            () =>
                                            {
                                                nextBtn.DOFade(1.0f, 0.35f).SetDelay(2.0f).SetEase(Ease.Linear)
                                                    .OnComplete
                                                    (
                                                        () => { }
                                                    );
                                            }
                                        );
                                    }
                                );
                            }
                        );
                    }
                );
            }
        );
    }

    public override void Start()
    {
        localPos1 = starVfx1.transform.position;
        localPos2 = starVfx2.transform.position;
        localPos3 = starVfx3.transform.position;
    }

    public override void Update()
    {
    }

    public void ClaimRW()
    {
        AudioManager.instance.btnSound.Play();

        EarnMulti();
    }

    public void NextLevel()
    {
        GameController.Instance.playFabManager.SendPlayTimeToPlayFab();
        //AudioManager.instance.btnSound.Play();
        GameManager.Instance.SaveStar();
        moveArrowTween.Pause();

             GameManager.Instance.NextLevel();
    }

    IEnumerator WaitVfx()
    {
        yield return new WaitForSeconds(1.5f);
        //GameManager.Instance.NextLevel();
        claimButton.interactable = true;
        nextButton.interactable = true;
    }

    public void PlayStarVfx()
    {
        AudioManager.instance.moreCoinSound.Play();
        starVfx1.SetActive(true);
        starVfx1.transform.DOMove(starVfxTarget.position, 0.5f).OnComplete(
            () =>
            {
                starVfx1.SetActive(false);
                starVfx1.transform.position = localPos1;
            }
        );

        starVfx2.gameObject.SetActive(true);
        starVfx2.transform.DOMove(starVfxTarget.position, 0.5f).SetDelay(0.25f).OnComplete(
            () =>
            {
                starVfx2.gameObject.SetActive(false);
                starVfx2.transform.position = localPos2;
            }
        );

        starVfx3.gameObject.SetActive(true);
        starVfx3.transform.DOMove(starVfxTarget.position, 0.5f).SetDelay(0.5f).OnComplete
        (
            () =>
            {
                starVfx3.gameObject.SetActive(false);
                starVfx3.transform.position = localPos3;
                gotStarTxt.text = (GameManager.Instance.inGameStar).ToString();
            }
        );
    }

    public void EarnMulti()
    {
        //Debug.Log("CLAIM MULTI STAR");
        GameManager.Instance.inGameStar *= multiAdsCount;
        GameManager.Instance.SaveStar();
        moveArrowTween.Pause();
        claimButton.interactable = false;
        nextButton.interactable = false;

        PlayStarVfx();
        StartCoroutine(WaitVfx());
    }
}