using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUDPage : PageBase
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Slider healthBar;

    public override void SetValues()
    {

    }

    public override void SetValuesOnSceneLoad()
    {
        healthBar.maxValue = PlayerManager.Stats.maxHealth;
        healthBar.value = PlayerManager.Stats.maxHealth;

        PlayerManager.Stats.OnChangeHealth += Stats_OnChangeHealth;

        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(() => UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<PausePage>()));
    }

    private void Stats_OnChangeHealth(float value)
    {
        healthBar.value = value;
    }
}