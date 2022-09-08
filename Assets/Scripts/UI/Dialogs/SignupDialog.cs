using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SignupDialog : DialogBase
{
    [Header("Buttons")]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button signupButton;
    [SerializeField] private Button loginButton;

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    private const string defaultEmail = "fantasyRPG@gmail.com";
    private const string defaultUsername = "";
    private const string defaultPassword = "";

    public override void SetValues()
    {
        emailInputField.text = defaultEmail;
        usernameInputField.text = defaultUsername;
        passwordInputField.text = defaultPassword;
    }

    public override void SetValuesOnSceneLoad()
    {
        closeButton.onClick.RemoveAllListeners();
        signupButton.onClick.RemoveAllListeners();
        loginButton.onClick.RemoveAllListeners();

        closeButton.onClick.AddListener(() =>
        {
            UI_Manager.instance.CloseDialog(this);
        });
        signupButton.onClick.AddListener(() =>
        {
            SignupAction();
            UI_Manager.instance.CloseDialog(this);
        });
        loginButton.onClick.AddListener(() =>
        {
            UI_Manager.instance.CloseDialog(this);
            UI_Manager.instance.OpenDialog(UI_Manager.instance.GetDialogOfType<LoginDialog>());
        });
    }

    private void SignupAction()
    {
        UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<HomePage>());
    }
}
