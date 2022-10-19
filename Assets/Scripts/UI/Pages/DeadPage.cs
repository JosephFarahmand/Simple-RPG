using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadPage : PageBase
{
    [SerializeField] private Button homeButton;
    [SerializeField] private Button reviveButton;

    public override void SetValues()
    {

    }

    public override void SetValuesOnSceneLoad()
    {
        homeButton.onClick.RemoveAllListeners();
        homeButton.onClick.AddListener(() =>
        {
            UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<HomePage>());
        });
    }
}
