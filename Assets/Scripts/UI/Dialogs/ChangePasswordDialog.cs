using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangePasswordDialog : DialogBase
{
    [SerializeField] private TMP_InputField oldPasswordinputField;
    [SerializeField] private TMP_InputField newPasswordinputField;
    [SerializeField] private Button okButton;

    [Space]
    [SerializeField] private WarningMassege warningMassege;

    public override void SetValues()
    {

    }

    public override void SetValuesOnSceneLoad()
    {
        var wrongPassword = GameManager.ErrorController.FindEntity(521);
        var changedSuccessfuly = GameManager.ErrorController.FindEntity(522);

        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(() =>
        {
            var oldPassword = oldPasswordinputField.text;
            var newPassword = newPasswordinputField.text;

            if (oldPassword != AccountController.Profile.Password)
            {
                warningMassege.SetWarning(wrongPassword);
            }
            else
            {
                AccountController.ChangePassword(newPassword);
                UI_Manager.instance.CloseDialog(this);
                GameManager.ErrorController.ShowError(changedSuccessfuly);
            }
        });

        //inputField.onValueChanged.AddListener((value) =>
        //{
        //    okButton.interactable = value != null && value != string.Empty && value != AccountController.Data.Username;
        //});
    }
}
