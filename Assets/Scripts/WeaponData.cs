using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float baseDamage;
    public float cooldown;
    public float attackDistance;
}