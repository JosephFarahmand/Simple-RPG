using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningMassege : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text messageText;

    public void SetWarning(int warningCode)
    {
        GameManager.ErrorController.ShowError(warningCode, this);
    }

    public void SetWarning(string title, string message)
    {
        titleText.SetText(title);
        messageText.SetText(message);
    }

    public void SetWarning(ErrorDatabase.ErrorEntity error)
    {
        icon.sprite = GameManager.ErrorController.GetIcon(error.ErrorType);
        titleText.SetText(error.Title);
        messageText.SetText(error.Message);
    }
}
