using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinuePage : PageBase
{
    [SerializeField] private Button continueButton;

    public override void SetValues()
    {

    }

    public override void SetValuesOnSceneLoad()
    {
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(() =>
        {
            ContinueAction();
            UI_Manager.instance.ClosePage(this);
        });
    }

    private void ContinueAction()
    {

    }
}
