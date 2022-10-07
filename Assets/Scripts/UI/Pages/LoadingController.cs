using System.Collections.Generic;
using System;

public static class LoadingController
{
    public static List<Action> callbacks;
    public static int callbacksCount => callbacks.Count;

    public static Action onLoadingComplete;

    public static void AddAction(Action callback)
    {
        if(callbacks == null)
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
}