using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Inventory/Resource")]
public class Resource : Item
{
    [SerializeField] private int count = 0;

    public int Count => count;
}
