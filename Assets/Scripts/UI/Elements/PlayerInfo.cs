using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : UIElementBase
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider levelSlider;

    public override void SetValues()
    {
        //set player level and name
    }
}
