using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePage : PageBase
{
    [SerializeField] private Button settingButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button homeButton;

    public override void SetValues()
    {
        GameManager.SetStats(GameManager.Stats.PauseGame);
    }

    public override void SetValuesOnSceneLoad()
    {
        settingButton.onClick.RemoveAllListeners();
        settingButton.onClick.AddListener(() => UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<SettingPage>()));

        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(() =>
        {
            ContinueAction();
            UI_Manager.instance.OnBackPressed();
        });

        homeButton.onClick.RemoveAllListeners();
        homeButton.onClick.AddListener(() =>
        {
            UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<HomePage>());
        });
    }

    private void ContinueAction()
    {
        GameManager.SetStats(GameManager.Stats.PlayGame);
    }
}
