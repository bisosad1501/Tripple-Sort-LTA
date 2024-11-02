using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenLoading : MonoBehaviour
{
    public Image loadingBar;

    public float timeLoading;

    private float fillAmount;

    public Text txtLoading;

    // Start is called before the first frame update
    private void Start()
    {
        fillAmount = 0.065f;
        Application.targetFrameRate = 60;
        loadingBar.fillAmount = 0.065f;
        StartCoroutine(ChangeScene());
        StartCoroutine(LoadingText());
    }
    // Update is called once per frame
    private IEnumerator ChangeScene()
    {
        GameController.Instance.isShowOpen = false;
        yield return new WaitForSeconds(1f);
        while (fillAmount < 1)
        {
            loadingBar.fillAmount = fillAmount;
            yield return new WaitForFixedUpdate();
            fillAmount += 0.01f;
            GameController.Instance.progressLoading = fillAmount;
        }
        if (!GameController.Instance.isShowOpen)
        {
            StartCoroutine(WaitChangeScene());
        }
        else
        {
            OpenGame();
        }
    }
    IEnumerator WaitChangeScene()
    {
        Debug.Log("chuanbiu");
        //// we switch to the new scene
        yield return new WaitForSeconds(1f);
        Debug.Log("LoadDone");
        OpenGame();
    }
    private IEnumerator LoadingText()
    {
        var wait = new WaitForSeconds(1f);
        while (true)
        {
            txtLoading.text = "Loading.";
            yield return wait;

            txtLoading.text = "Loading..";
            yield return wait;

            txtLoading.text = "Loading...";
            yield return wait;
        }
    }
    void OpenGame()
    {
        gameObject.SetActive(false);
        if(GameManager.Instance.currentLevel == 1) GameManager.Instance.uiManager.HomeLayer.PlayGame(true);
    }
}
