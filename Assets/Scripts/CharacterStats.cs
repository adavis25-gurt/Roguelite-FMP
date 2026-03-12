using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestItem
{
    public ItemData data;
    public int amount;
}

public class CharacterStats : MonoBehaviour
{
    public static CharacterStats Instance { get; private set; }

    public Stat health;
    public Stat attackDamage  = new Stat(10f);
    public Stat attackSpeed   = new Stat(1f);
    public Stat defense       = new Stat(5f);
    public Stat jumpPower     = new Stat(8f);
    public Stat moveSpeed     = new Stat(16f);
    public Stat jumpAmount    = new Stat(1f);
    public Stat expMultiplier = new Stat(1f);

    [Header("Debug")]
    public List<TestItem> testItems;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        health = new Stat(PlayerStatsManager.Instance.health.baseValue);
    }

    void Start()
    {
        foreach (TestItem testItem in testItems)
            for (int i = 0; i < testItem.amount; i++)
                ApplyItem(testItem.data);
                Debug.Log($"Move Speed: {moveSpeed.GetValue()}");
    }

    public void ApplyItem(ItemData data)
    {
        StatModifier modifier = new StatModifier(data.value, data.modifierType);

        switch (data.statType)
        {
            case StatType.Health:
                health.AddModifier(modifier);
                break;
            case StatType.AttackDamage:
                attackDamage.AddModifier(modifier);
                break;
            case StatType.AttackSpeed:
                attackSpeed.AddModifier(modifier);
                break;
            case StatType.Defense:
                defense.AddModifier(modifier);
                break;
            case StatType.JumpPower:
                jumpPower.AddModifier(modifier);
                break;
            case StatType.MoveSpeed:
                moveSpeed.AddModifier(modifier);
                break;
            case StatType.JumpAmount:
                jumpAmount.AddModifier(modifier);
                break;
            case StatType.ExpMultiplier:
                expMultiplier.AddModifier(modifier);
                break;
        }
    }
}
