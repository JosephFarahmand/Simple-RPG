using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PageBase : MonoBehaviour
{
    [Header("Base Info")]
    [SerializeField] private string title;
    public List<UIElementBase> elements;
    [SerializeField] public UnityEvent onOpen;
    [SerializeField] private bool loadAtFirst = false;

    public string Title
    {
        get
        {
            if (title == null || title == string.Empty)
            {
                title = gameObject.name;
            }
            return title;
        }
    }

    public bool LoadAtFirst { get => loadAtFirst; }

    /// <summary>
    /// call when open this panel
    /// </summary>
    public abstract void SetValues();

    /// <summary>
    /// call on load game
    /// </summary>
    public abstract void SetValuesOnSceneLoad();

    protected virtual void OnEnable()
    {
        onOpen?.Invoke();
    }
}

