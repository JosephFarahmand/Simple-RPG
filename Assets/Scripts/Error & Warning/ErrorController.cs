using UnityEngine;
using System;
using System.Collections.Generic;

public class ErrorController : MonoBehaviour, IController
{
    [SerializeField] private List<ErrorDatabase> databases;

    [Header("Icons")]
    [SerializeField] private Sprite errorIcon;
    [SerializeField] private Sprite warningIcon;
    [SerializeField] private Sprite messageIcon;
    [SerializeField] private Sprite hintIcon;


    public void Initialization()
    {
        LoadingController.onLoadingError += (errorCode) =>
        {
            ShowError(errorCode, retryCallback : () => LoadingController.LoadAction());
        };
    }

    public Sprite GetIcon(ErrorDatabase.ErrorType type)
    {
        return type switch
        {
            ErrorDatabase.ErrorType.Error => errorIcon,
            ErrorDatabase.ErrorType.Warning => warningIcon,
            ErrorDatabase.ErrorType.Message => messageIcon,
            ErrorDatabase.ErrorType.Hint => hintIcon,
            _ => messageIcon,
        };
    }

    public ErrorDatabase.ErrorEntity FindEntity(int code)
    {
        foreach (var database in databases)
        {
            var entity = database.GetError(code);
            if (entity.Code != 0)
            {
                return entity;
            }
        }
        Debug.LogError($"This code is not available in the database. (Code={code})");
        return default;
    }

    public void ShowError(int code, Action cancelCallback = null, Action retryCallback = null)
    {
        var errorData = FindEntity(code);

        if (errorData.Code == 0)
        {
            Debug.LogError($"This code is not available in the database. (Code={code})");
            return;
        }

        ShowError(errorData, cancelCallback, retryCallback);
    }

    public void ShowError(ErrorDatabase.ErrorEntity error, Action cancelCallback = null, Action retryCallback = null)
    {
        // display a error dialog
        var errorDialog = UI_Manager.instance.GetDialogOfType<ErrorDialog>();
        errorDialog.SetValues(error, cancelCallback, retryCallback);
        UI_Manager.instance.OpenDialog(errorDialog);
    }

    public void ShowError(int code, WarningMassege warningMassege)
    {
        var errorData = FindEntity(code);

        if (errorData.Code == 0)
        {
            Debug.LogError($"This code is not available in the database. (Code={code})");
            return;
        }

        warningMassege.SetWarning(errorData);
    }
}
