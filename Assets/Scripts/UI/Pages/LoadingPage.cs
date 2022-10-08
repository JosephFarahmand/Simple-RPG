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
        LoadingController.LoadAction();
    }

    public override void SetValuesOnSceneLoad()
    {
        LoadingController.onLoadingProgressCallback += LoadingProgress;
    }

    private void LoadingProgress(int currentIndex, int maxCallbacksCount)
    {
        loadingBar.maxValue = maxCallbacksCount;
        var val = currentIndex + 1;
        loadingBar.value = val;
        loadingValueText.SetText($"Loading... {val * 100 / maxCallbacksCount}%");
    }
}
