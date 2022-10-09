using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class LoadingController
{
    private static List<Action> callbacks;

    public static Action onLoadingComplete;

    public delegate void OnLoadingProgress(int currentIndex, int maxCallbacksCount);
    public static OnLoadingProgress onLoadingProgressCallback;
    public static Action<int> onLoadingError;

    public static void AddAction(Action callback)
    {
        if (callbacks == null)
        {
            callbacks = new List<Action>();
        }
        callbacks.Add(callback);
    }

    public static void Complete()
    {
        onLoadingComplete?.Invoke();
        callbacks.Clear();
    }

    public static async void LoadAction()
    {
        UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<LoadingPage>());
        try
        {
            for (int i = 0; i < callbacks.Count; i++)
            {
                Action callback = callbacks[i];
                await Task.Delay(UnityEngine.Random.Range(100, 150));
                callback?.Invoke();
                onLoadingProgressCallback?.Invoke(i, callbacks.Count);
            }
        }
        catch (Exception)
        {
            onLoadingError?.Invoke(600);
            throw;
        }

        Complete();
    }
}