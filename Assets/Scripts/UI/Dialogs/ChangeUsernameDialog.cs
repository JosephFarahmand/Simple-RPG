using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUsernameDialog : DialogBase
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button okButton;

    [Space]
    [SerializeField] private WarningMassege warningMassege;

    public override void SetValues()
    {

    }

    public override void SetValuesOnSceneLoad()
    {
        var invalideUsernameError = GameManager.ErrorController.FindEntity(520);
        var changedSuccessfuly = GameManager.ErrorController.FindEntity(522);

        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(() =>
        {
            var accept = AccountController.ChangeUsername(inputField.text);
            if (accept)
            {
                UI_Manager.instance.CloseDialog(this);
                GameManager.ErrorController.ShowError(changedSuccessfuly);
            }
            else
            {
                warningMassege.SetWarning(invalideUsernameError);
            }
        });

        inputField.onValueChanged.AddListener((value) =>
        {
            okButton.interactable = value != null && value != string.Empty && value != AccountController.Profile.Username;
        });
    }
}
