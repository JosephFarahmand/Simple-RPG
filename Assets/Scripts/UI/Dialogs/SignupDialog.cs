using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public override void SetValues()
    {
        emailInputField.text = StaticData.defaultEmail;
        usernameInputField.text = StaticData.defaultUsername;
        passwordInputField.text = StaticData.defaultPassword;
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
                UI_Manager.instance.CloseDialog(this);
            }
            else
            {
                Debug.LogError("Login failed");
            }
        });
        loginButton.onClick.AddListener(() =>
        {
            UI_Manager.instance.CloseDialog(this);
            UI_Manager.instance.OpenDialog(UI_Manager.instance.GetDialogOfType<LoginDialog>());
        });

        int isEmailValidate = 0, isUsernameValidate = 0, isPasswordValidate = 0;

        emailInputField.onValueChanged.AddListener((value) =>
        {
            isEmailValidate = IsValidEmail(value) && value != StaticData.defaultEmail ? 1 : 0;
            signupButton.interactable = isEmailValidate * isUsernameValidate * isPasswordValidate == 1;
        });
        usernameInputField.onValueChanged.AddListener((value) =>
        {
            isUsernameValidate = value != null && value != string.Empty && value != StaticData.defaultUsername ? 1 : 0;
            signupButton.interactable = isEmailValidate * isUsernameValidate * isPasswordValidate == 1;
        });
        passwordInputField.onValueChanged.AddListener((value) =>
        {
            isPasswordValidate = value != null && value != string.Empty && value != StaticData.defaultPassword ? 1 : 0;
            signupButton.interactable = isEmailValidate * isUsernameValidate * isPasswordValidate == 1;
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
