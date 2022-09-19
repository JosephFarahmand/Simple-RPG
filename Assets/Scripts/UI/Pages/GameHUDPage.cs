using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUDPage : PageBase
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Slider healthSlider;

    public override void SetValues()
    {

    }

    public override void SetValuesOnSceneLoad()
    {
        healthSlider.maxValue = PlayerManager.Stats.maxHealth;
        healthSlider.value = PlayerManager.Stats.maxHealth;

        PlayerManager.Stats.OnChangeHealth += Stats_OnChangeHealth;

        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(() => UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<PausePage>()));
    }

    private void Stats_OnChangeHealth(float maxHealth, float currentHealth)
    {
        healthSlider.value = currentHealth;
    }
}