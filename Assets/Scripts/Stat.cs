using System.Collections.Generic;
using UnityEngine;

public enum ModifierType
{
    Flat,
    Percent
}

public class StatModifier
{
    public float value;
    public ModifierType type;

    public StatModifier(float value, ModifierType type)
    {
        this.value = value;
        this.type = type;
    }
}

[System.Serializable]
public class Stat
{
    public float baseValue;
    [SerializeField] private float currentValue;
    private List<StatModifier> modifiers = new List<StatModifier>();

    public Stat(float baseValue)
    {
        this.baseValue = baseValue;
        currentValue = baseValue;
    }

    public void AddModifier(StatModifier modifier)
    {
        modifiers.Add(modifier);
        currentValue = GetValue();
    }

    public float GetValue()
    {
        float flat = 0f;
        float percent = 0f;

        foreach (StatModifier mod in modifiers)
        {
            if (mod.type == ModifierType.Flat)
                flat += mod.value;
            else if (mod.type == ModifierType.Percent)
                percent += mod.value;
        }

        return (baseValue + flat) * (1 + percent);
    }
}
