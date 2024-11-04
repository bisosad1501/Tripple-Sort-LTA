using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoginView : BaseView
{
    [SerializeField] Button loginBtn;
    [SerializeField] Button closeBtn;
    [SerializeField] InputField userNameField;
    private string userName;
    public override void Start()
    {
        
    }

    public override void Update()
    {
       
    }

    public override void InitView()
    {
        loginBtn.onClick.AddListener(Login);
        closeBtn.onClick.AddListener(HideViewHome);
        userNameField.onEndEdit.AddListener((txt) =>
        {
            userName = txt;
        });
    }

    public override void ShowView()
    {
        InitView();
        base.ShowView();
    }

    public void Login()
    {
        Debug.Log("Login");
        GameController.Instance.playFabManager.ChangeName(userName);
        HideViewHome();
    }
}
