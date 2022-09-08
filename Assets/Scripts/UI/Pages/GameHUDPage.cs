using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUDPage : PageBase
{
    [SerializeField] private Button pauseButton;

    public override void SetValues()
    {

    }

    public override void SetValuesOnSceneLoad()
    {
        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(() => UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<PausePage>()));
    }
}
