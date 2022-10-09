using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerInfo : UIElementBase
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Button editUsernameButton;

    [Header("XP bar")]
    [SerializeField] private Slider xpSlider;
    [SerializeField] private TMP_Text xpValueText;

    public override void SetValues()
    {
    }

    public override void SetValuesOnSceneLoad()
    {
        //set player level and name
        nameText.SetText(AccountController.Data.Username);
        levelText.SetText(AccountController.Data.Level.ToString());

        AccountController.onChangeProperty += ChangeProperty;

        editUsernameButton.onClick.RemoveAllListeners();
        editUsernameButton.onClick.AddListener(() =>
        {
            UI_Manager.instance.OpenDialog(UI_Manager.instance.GetDialogOfType<ChangeUsernameDialog>());
        });
    }

    private void ChangeProperty(PlayerProfile profile)
    {
        nameText.SetText(profile.Username);
        levelText.SetText(profile.Level.ToString());

        xpSlider.maxValue = profile.XP.MaximumValue;
        xpSlider.value = profile.XP.CurrentValue;
        xpValueText.SetText($"{profile.XP.CurrentValue}/{profile.XP.MaximumValue}");
    }
}
