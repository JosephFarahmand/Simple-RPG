using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingPage : PageBase
{
    [Header("Buttons")]
    [SerializeField] private Button languageButton;
    [SerializeField] private Button likeButton;
    [SerializeField] private Button aboutButton;
    [SerializeField] private Button logOutButton;

    [Header("Sound & SFX")]
    [SerializeField] private SliderElement music;
    [SerializeField] private SliderElement sfx;

    public override void SetValues()
    {

    }

    public override void SetValuesOnSceneLoad()
    {
        music.SetValue(ChangeMusic);
        sfx.SetValue(ChangeSFX);

        // load music and sfx from save or load manager and set values
        music.Setup(0);
        sfx.Setup(0);

        languageButton.onClick.RemoveAllListeners();
        languageButton.onClick.AddListener(() => UI_Manager.instance.OpenDialog(UI_Manager.instance.GetDialogOfType<LanguageDialog>()));

        logOutButton.onClick.RemoveAllListeners();
        logOutButton.onClick.AddListener(() =>
        {
            LogOutAction();
            UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<EntryPage>());
        });

        likeButton.onClick.RemoveAllListeners();
        likeButton.onClick.AddListener(() => Application.OpenURL(StaticData.likeURL));

        aboutButton.onClick.RemoveAllListeners();
        aboutButton.onClick.AddListener(() => Application.OpenURL(StaticData.aboutURL));
    }

    private void LogOutAction()
    {
        AccountController.Logout();
    }

    private void ChangeSFX(float value)
    {
    }

    private void ChangeMusic(float value)
    {
    }

    [Serializable]
    private class SliderElement
    {
        [SerializeField] private Slider slider;
        [SerializeField] private GameObject onMode;
        [SerializeField] private GameObject offMode;

        [SerializeField] private TMP_Text minValueText;
        [SerializeField] private TMP_Text maxValueText;

        public void SetValue(Action<float> callback)
        {
            slider.onValueChanged.AddListener((value) =>
            {
                offMode.SetActive(false);
                onMode.SetActive(false);
                if (value == 0)
                {
                    offMode.SetActive(true);
                }
                else
                {
                    onMode.SetActive(true);
                }

                callback?.Invoke(value);
            });
        }

        public void Setup(float value)
        {
            slider.value = value;
            minValueText.text = slider.minValue.ToString();
            maxValueText.text = slider.maxValue.ToString();
        }
    }
}
