using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : UIElementBase
{
    [SerializeField] private Element coin;
    [SerializeField] private Element gem;

    public override void SetValues()
    {
    }

    public override void SetValuesOnSceneLoad()
    {
        //get player coin and gem value and set theme
        coin.SetValue(AccountController.Profile.CoinAmount, () =>
        {

        });

        gem.SetValue(AccountController.Profile.GemAmount, () =>
        {

        });

        AccountController.onChangeProperty += ChangeProperty;
    }

    private void ChangeProperty(PlayerProfile profile)
    {
        coin.SetValue(profile.CoinAmount);
        gem.SetValue(profile.GemAmount);
    }

    [Serializable]
    public struct Element
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Button button;

        public void SetValue(float value)
        {
            text.text = value.ToString();
        }

        public void SetValue(float value, Action callback)
        {
            text.text = value.ToString();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => callback?.Invoke());
        }
    }
}
