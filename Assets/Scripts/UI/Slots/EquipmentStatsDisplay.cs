using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentStatsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text valueText;

    public void SetValue(int value, string title)
    {
        titleText.SetText(title);
        valueText.SetText($"+{value}");
    }
}
