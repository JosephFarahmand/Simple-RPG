using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentHandler : MonoBehaviour
{
    [SerializeField] private Equipment item;

    public string ID => item.Id;
    public Equipment Item { get => item; set => item = value; }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
