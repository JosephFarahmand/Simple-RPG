using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningMassege : MonoBehaviour
{
    [SerializeField, HideIf(nameof(onlyTitle))] private Image icon;
    [SerializeField] private TMP_Text titleText;
    [SerializeField, HideIf(nameof(onlyTitle))] private TMP_Text messageText;

    [SerializeField] private bool onlyTitle = false;

    [Header("Display Timer")]
    [SerializeField] private bool userTimer = false;
    [SerializeField, Min(0),ShowIf(nameof(userTimer))] private float displayTime = 3;
    private float currentTime = 0;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    [System.Obsolete]
    public void SetWarning(int warningCode)
    {
        GameManager.ErrorController.ShowError(warningCode, this);
    }

    [System.Obsolete]
    public void SetWarning(string title, string message)
    {
        gameObject.SetActive(true);
        icon.enabled = false;
        titleText.SetText(title);
        if (!onlyTitle)
        {
            messageText.SetText(message);
        }

        if (userTimer)
        {
            currentTime = displayTime;
        }
    }

    public void SetWarning(ErrorDatabase.ErrorEntity error)
    {
        gameObject.SetActive(true);
        if (!onlyTitle)
        {
            icon.sprite = GameManager.ErrorController.GetIcon(error.ErrorType);
            messageText.SetText(error.Message);
        }
        titleText.SetText(error.Title);

        if (userTimer)
        {
            currentTime = displayTime;
        }
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        if (!userTimer) return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
