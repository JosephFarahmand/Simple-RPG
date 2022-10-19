using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePage : PageBase
{
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;

    public override void SetValues()
    {
        GameManager.SetStats(GameManager.Stats.InHome);
    }

    public override void SetValuesOnSceneLoad()
    {
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() =>
        {
            UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<GameHUDPage>());
            PlayGame();
        });

        settingButton.onClick.RemoveAllListeners();
        settingButton.onClick.AddListener(() => UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<SettingPage>()));
    }

    private void PlayGame()
    {
        GameManager.SetStats(GameManager.Stats.PlayGame);
    }
}
