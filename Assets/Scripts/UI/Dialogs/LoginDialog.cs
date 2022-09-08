using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LoginDialog : DialogBase
{
    [Header("Buttons")]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button signupButton;
    [SerializeField] private Button forgotPasswordButton;
    [SerializeField] private Button loginButton;

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    [Header("Toggle")]
    [SerializeField] private Toggle rememberMeToggle;

    private const string defaultUsername = "Fantasy RPG";
    private const string defaultPassword = "";

    public override void SetValues()
    {
        usernameInputField.text = defaultUsername;
        passwordInputField.text = defaultPassword;
    }

    public override void SetValuesOnSceneLoad()
    {
        closeButton.onClick.RemoveAllListeners();
        signupButton.onClick.RemoveAllListeners();
        loginButton.onClick.RemoveAllListeners();
        forgotPasswordButton.onClick.RemoveAllListeners();

        closeButton.onClick.AddListener(() =>
        {
            UI_Manager.instance.CloseDialog(this);
        }); 
        signupButton.onClick.AddListener(() =>
        {
            UI_Manager.instance.CloseDialog(this);
            UI_Manager.instance.OpenDialog(UI_Manager.instance.GetDialogOfType<SignupDialog>());
        });
        forgotPasswordButton.onClick.AddListener(() => Debug.Log("NOTHING"));
        loginButton.onClick.AddListener(() =>
        {
            LoginAction();
            UI_Manager.instance.CloseDialog(this);
        });
    }

    private void LoginAction()
    {
        UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<HomePage>());
    }
}
