using UnityEngine;

public enum StatType
{
    None = 0,
    Health,
    AttackDamage,
    AttackSpeed,
    Defense,
    JumpPower,
    MoveSpeed,
    JumpAmount,
    ExpMultiplier,
    ProjectileCount,
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public StatType statType;
    public float value;
    public ModifierType modifierType;
}
