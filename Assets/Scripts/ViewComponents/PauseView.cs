using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PauseView : BaseView
{
    public GameObject musicOn, musicOff;

    public GameObject soundOn, soundOff;

    public GameObject vibrationOn, vibrationOff;

    public override void InitView()
    {
        if (AudioManager.instance.musicState == 0)
        {
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
        else
        {
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }

        if (AudioManager.instance.soundState == 0)
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
        else
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }

        if (AudioManager.instance.hapticState == 0)
        {
            vibrationOn.SetActive(false);
            vibrationOff.SetActive(true);
        }
        else
        {
            vibrationOn.SetActive(true);
            vibrationOff.SetActive(false);
        }
    }

    public override void Start()
    {
       
    }

    public override void Update()
    {
        
    }

    public override void ShowView()
    {
        base.ShowView();
        InitView();
        GameManager.Instance.currentState = GameManager.GAME_STATE.IN_GAME_POPUP;
    }


    IEnumerator ResumeIE()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.Instance.currentState = GameManager.GAME_STATE.IN_GAME;
    }

    public void Continue()
    {
        
            AudioManager.instance.btnSound.Play();
            HideView();
            GameManager.Instance.currentState = GameManager.GAME_STATE.IN_GAME;
      
    }

    public void Replay()
    {
        AudioManager.instance.btnSound.Play();
       
            HideView();
            GameManager.Instance.ReplayLevel();   
    }

    public void BackToHome()
    {
        AudioManager.instance.btnSound.Play();
        
            HideViewBack();
            GameManager.Instance.BackToHome();
   
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
                GameManager.Instance.currentState = GameManager.GAME_STATE.IN_HOME;
            });
    }
    

    public void ToggleSound()
    { 
        
            if (AudioManager.instance.soundState == 0)
            {
                soundOn.SetActive(true);
                soundOff.SetActive(false);
                AudioManager.instance.ToogleSound(true);
                AudioManager.instance.btnSound.Play();
            }
            else
            {
                soundOn.SetActive(false);
                soundOff.SetActive(true);
                AudioManager.instance.ToogleSound(false);
                AudioManager.instance.btnSound.Play();

            }
        
    }

    public void ToggleMusic()
    {
            if (AudioManager.instance.musicState == 0)
            {
                musicOn.SetActive(true);
                musicOff.SetActive(false);
                AudioManager.instance.ToogleMusic(true);
                AudioManager.instance.btnSound.Play();

            }
            else
            {
                musicOn.SetActive(false);
                musicOff.SetActive(true);
                AudioManager.instance.ToogleMusic(false);
                AudioManager.instance.btnSound.Play();

            }
        
    }

    public void ToggleHaptic()
    {
            if (AudioManager.instance.hapticState == 0)
            {
                vibrationOn.SetActive(true);
                vibrationOff.SetActive(false);
                AudioManager.instance.ToogleHaptic(true);
                AudioManager.instance.btnSound.Play();

            }
            else
            {
                vibrationOn.SetActive(false);
                vibrationOff.SetActive(true);
                AudioManager.instance.ToogleHaptic(false);
                AudioManager.instance.btnSound.Play();

            }
    }
}
