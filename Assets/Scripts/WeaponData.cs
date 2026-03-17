using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string WeaponName;
    public StatType statType1, statType2;
    public float value1, value2;
    public ModifierType modifierType1, modifierType2;
    public float cooldown;
    public float attackDistance;
}
