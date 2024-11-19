using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class ErrorView : BaseView
{
    public Text errorText;

    public override void Start()
    {
        ClearError();
        HideView();
    }

    public void OnError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
        DisplayError(error.GenerateErrorReport());
        ShowView();
    }

    private void DisplayError(string errorMessage)
    {
        if (errorText != null)
        {
            errorText.text = errorMessage;
        }
        else
        {
            Debug.LogWarning("The Text element is not assigned.");
        }
    }

    public void ClearError()
    {
        if (errorText != null)
        {
            errorText.text = "";
        }
    }

    public override void InitView()
    {
        ClearError();
    }

    public override void ShowView()
    {
        base.ShowView();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public override void HideView()
    {
        base.HideView();
        ClearError();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public override void Update()
    {
    }
}