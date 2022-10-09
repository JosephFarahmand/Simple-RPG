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

    public override void SetValues()
    {
        usernameInputField.text = StaticData.defaultUsername;
        passwordInputField.text = StaticData.defaultPassword;
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
            var accept= AccountController.Login(usernameInputField.text, passwordInputField.text, rememberMeToggle.isOn);
            if (accept)
            {
                UI_Manager.instance.CloseDialog(this);
            }
        });

        int isUsernameValidate = 0, isPasswordValidate = 0;

        usernameInputField.onValueChanged.AddListener((value) =>
        {
            isUsernameValidate = value != null && value != string.Empty && value != StaticData.defaultUsername ? 1 : 0;
            loginButton.interactable = isUsernameValidate * isPasswordValidate == 1;
        });
        passwordInputField.onValueChanged.AddListener((value) =>
        {
            isPasswordValidate = value != null && value != string.Empty && value != StaticData.defaultPassword ? 1 : 0;
            loginButton.interactable = isUsernameValidate * isPasswordValidate == 1;
        });
    }
}
