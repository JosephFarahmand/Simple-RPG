using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageDialog : DialogBase
{
    [SerializeField] private Button exitButton; 

    [SerializeField] private List<LanguageElement> languageElements;

    public override void SetValues()
    {

    }

    public override void SetValuesOnSceneLoad()
    {
        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(() => UI_Manager.instance.CloseDialog(this));
    }


    [System.Serializable]
    private struct LanguageElement
    {
        // get available language from language manager and chice from there
        // enum languages

        [SerializeField] private GameObject ckeckObject;

        public void SetValue(bool selected)
        {
            ckeckObject.SetActive(selected);
        }
    }
}
