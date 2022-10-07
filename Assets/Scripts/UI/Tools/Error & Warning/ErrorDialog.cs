using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ErrorDialog : DialogBase
{
    [Header("Text")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text messageText;

    [Header("Button")]
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button acceptButton;

    [Header("Icon")]
    [SerializeField] private Image icon;
    public override void SetValues()
    {
        
    }

    public override void SetValuesOnSceneLoad()
    {
        
    }

    public void SetValues(ErrorDatabase.ErrorEntity errorEntity, Action cancelCallback = null, Action acceptCallback = null)
    {
        if (errorEntity.Title == null)
        {
            titleText.SetText(errorEntity.ErrorType.ToString());
        }
        else
        {
            titleText.SetText(errorEntity.Title);
        }

        if (errorEntity.Message == null)
        {
            messageText.gameObject.SetActive(false);
        }
        else
        {
            messageText.gameObject.SetActive(true);
            messageText.SetText(errorEntity.Message);
        }

icon.sprite = GameManager.ErrorController.GetIcon(errorEntity.ErrorType);

        if (cancelCallback == null)
        {
            cancelButton.gameObject.SetActive(false);
        }
        else
        {
            cancelButton.gameObject.SetActive(true);
            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(() => cancelCallback.Invoke());
        }

        if (acceptCallback == null)
        {
            acceptButton.gameObject.SetActive(false);
        }
        else
        {
            acceptButton.gameObject.SetActive(true);
            acceptButton.onClick.RemoveAllListeners();
            acceptButton.onClick.AddListener(() => acceptCallback.Invoke());
        }
    }


}
