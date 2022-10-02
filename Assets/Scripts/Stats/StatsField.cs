using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsField
{
    [SerializeField] private float baseValue;

    private List<int> modifiers = new List<int>();

    public float GetValue()
    {
        var finalValue = baseValue;
        modifiers.ForEach(modifier => finalValue += modifier);
        return finalValue;
    }

    public void AddModifier(int modifier)
    {
        if(modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }
}