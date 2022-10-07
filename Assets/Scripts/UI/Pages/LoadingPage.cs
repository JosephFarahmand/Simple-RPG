using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System;

public class LoadingPage : PageBase
{
    [SerializeField] private Slider loadingBar;
    [SerializeField] private TMP_Text loadingValueText;

    public override void SetValues()
    {
        LoadAction();
    }

    public override void SetValuesOnSceneLoad()
    {

    }

    private async void LoadAction()
    {
        loadingBar.maxValue = LoadingController.callbacksCount;
        for (int i = 0; i < LoadingController.callbacks.Count; i++)
        {
            Action callback = LoadingController.callbacks[i];
            await Task.Delay(UnityEngine.Random.Range(100, 150));
            callback?.Invoke();
            
            var val = i + 1;
            loadingBar.value = val;
            loadingValueText.SetText($"Loading... {val * 100 / LoadingController.callbacksCount}%");
        }

        LoadingController.Complete();
    }
}
