using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class TabGroup : MonoBehaviour
{
    public ToggleGroup toggleGroup;

    [SerializeField] private List<TabToggle> tabToggles;

    public System.Action<int> onTabsAction;

    public void Subscribe(TabToggle tabToggle)
    {
        //if (tabToggles == null)
        //{
        //    tabToggles = new List<TabToggle>();
        //}

        //tabToggles.Add(tabToggle);
        //var index = tabToggles.IndexOf(tabToggle);
        //tabToggle.Init((val) => onTabsAction?.Invoke(index), toggleGroup);

        for (int i = 0; i < tabToggles.Count; i++)
        {
            var toggle = tabToggles[i];
            toggle.transform.SetSiblingIndex(i);
        }
    }

    public void ChangeTab(int index)
    {
        onTabsAction?.Invoke(index);
    }

    internal void Initialization(Queue<int> indexs)
    {
        foreach (var tabToggle in tabToggles)
        {
            var index = indexs.Dequeue();
            tabToggle.Init((val) => onTabsAction?.Invoke(index), toggleGroup);
        }
    }
}
