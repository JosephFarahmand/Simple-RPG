using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class DialogBase : MonoBehaviour
{
    [SerializeField] private string title;
    public List<UIElementBase> elements;
    public UnityEvent onOpen;
    [SerializeField] private bool loadAtFirst = false;
    public string Title
    {
        get
        {
            if (title == null || title == string.Empty)
            {
                title = gameObject.name.ToUpper();
            }
            return title;
        }
    }

    public bool LoadAtFirst => loadAtFirst;

    public virtual void OnEnable()
    {
        onOpen?.Invoke();
    }
    public abstract void SetValues();

    public abstract void SetValuesOnSceneLoad();
}
