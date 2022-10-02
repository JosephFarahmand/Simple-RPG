using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class BackButton : UIElementBase
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Button button;

    public override void SetValues()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(UI_Manager.instance.OnBackPressed);

        titleText.text = UI_Manager.instance.GetActivePanelTitle().ToUpper();
    }

    public override void SetValuesOnSceneLoad()
    {

    }

    public void SetValues(string title)
    {
        titleText.text = title;
    }
}
