using UnityEngine;

public enum StatType
{
    Health,
    AttackDamage,
    AttackSpeed,
    Defense,
    JumpPower,
    MoveSpeed,
    JumpAmount,
    ExpMultiplier
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public StatType statType;
    public float value;
    public ModifierType modifierType;
}
