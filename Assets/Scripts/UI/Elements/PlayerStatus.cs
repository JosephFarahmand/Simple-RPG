using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatus : UIElementBase
{
    [SerializeField] private Element coin;
    [SerializeField] private Element gem;

    public override void SetValues()
    {
        //get player coin and gem value and set theme
    }

    [System.Serializable]
    public struct Element
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Button button;

        public void SetValue(float value, System.Action callback)
        {
            text.text = value.ToString();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => callback?.Invoke());
        }
    }
}
