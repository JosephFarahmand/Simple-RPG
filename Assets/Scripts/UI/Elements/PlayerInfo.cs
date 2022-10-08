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
    [SerializeField] private Slider levelSlider;

    public override void SetValues()
    {
    }

    public override void SetValuesOnSceneLoad()
    {
        //set player level and name
        nameText.SetText(AccountController.Data.Username);
        levelText.SetText(AccountController.Data.Level.ToString());

        AccountController.onChangeProperty += ChangeProperty;
    }

    private void ChangeProperty(PlayerProfile profile)
    {
        nameText.SetText(profile.Username);
        levelText.SetText(profile.Level.ToString());
    }
}
