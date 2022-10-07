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
        return default;
    }

    public void ShowError(int code, Action cancelCallback = null, Action acceptCallback = null)
    {
        var errorData = FindEntity(code);

        if (errorData.Code == 0)
        {
            Debug.LogError($"This code is not available in the database. (Code={code})");
            return;
        }

        // display a error dialog
        var errorDialog = UI_Manager.instance.GetDialogOfType<ErrorDialog>();
        errorDialog.SetValues((ErrorDatabase.ErrorEntity)errorData, cancelCallback, acceptCallback);
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

        warningMassege.SetWarning((ErrorDatabase.ErrorEntity)errorData);
    }
}
