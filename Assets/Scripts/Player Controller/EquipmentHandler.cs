using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentHandler : MonoBehaviour
{
    [SerializeField] private Equipment item;

    public string ID => item.Id;
    public Equipment Item => item;

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}

public class EquipmentPositionHandler : MonoBehaviour
{
    [SerializeField] private EquipmentSlot slot;

    public void AddItem()
    {

    }
}
