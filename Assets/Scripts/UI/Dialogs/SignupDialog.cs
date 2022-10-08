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
            var accept = AccountController.SignUp(emailInputField.text, usernameInputField.text, passwordInputField.text);
            if (accept)
            {
                UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<LoadingPage>());
                UI_Manager.instance.CloseDialog(this);
            }
        });
        loginButton.onClick.AddListener(() =>
        {
            UI_Manager.instance.CloseDialog(this);
            UI_Manager.instance.OpenDialog(UI_Manager.instance.GetDialogOfType<LoginDialog>());
        });


        emailInputField.onValueChanged.AddListener((value) =>
        {   
            if (!IsValidEmail(value) || value == defaultEmail)
            {
                signupButton.interactable = false;
            }
            else
            {
                signupButton.interactable = true;
            }
        });
    }

    bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false; // suggested by @TK-421
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}
