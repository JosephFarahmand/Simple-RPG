using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Toggle))]
public class TabToggle : MonoBehaviour
{
    public TabGroup tabGroup;

    Toggle myToggle;

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private GameObject tabFocus;
    [SerializeField] private Color focusColor;
    [SerializeField] private Color normalColor;

    private void Start()
    {
        tabGroup.Subscribe(this);
        RemoveFocus();
    }

    public void Init(ToggleGroup group, UnityAction<bool> call,bool setInitFocus)
    {
        myToggle = GetComponent<Toggle>();
        myToggle.onValueChanged.RemoveAllListeners();
        myToggle.onValueChanged.AddListener((value) =>
        {
            call?.Invoke(value);

            if (value)
            {
                OnFocus();
            }
            else
            {
                RemoveFocus();
            }
        });
        myToggle.group = group;

        if (setInitFocus)
        {
            OnFocus();
        }
    }

    private void OnFocus()
    {
        titleText.color = focusColor;
        tabFocus.SetActive(true);
    }

    private void RemoveFocus()
    {
        titleText.color = normalColor;
        tabFocus.SetActive(false);
    }
}
