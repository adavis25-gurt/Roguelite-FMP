using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class WeaponSlot
{
    public WeaponData data;
    [HideInInspector] public float cooldownTimer = 0f;
}

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject playerObject;
    public WeaponSlot weapon;

    void Attack(GameObject enemy)
    {
        EnemyStats stats = enemy.GetComponent<EnemyStats>();
        if (!stats) return;

        float damage = weapon.data.baseDamage + PlayerStatsManager.Instance.attackDamage.GetValue();

        bool isCrit = Random.Range(0f, 100f) < PlayerStatsManager.Instance.CritChance.GetValue();
        if (isCrit)
            damage *= 1f + (PlayerStatsManager.Instance.CritDamage.GetValue() / 100f);

        stats.TakeDamage(damage);
    }

    void Update()
    {
        if (weapon.data == null) return;
        if (weapon.cooldownTimer > 0f) { weapon.cooldownTimer -= Time.deltaTime; return; }

        Collider[] hits = Physics.OverlapSphere(playerObject.transform.position, weapon.data.attackDistance, enemyLayer);

        foreach (Collider hit in hits)
            Attack(hit.gameObject);

        if (hits.Length > 0)
        {
            weapon.cooldownTimer = weapon.data.cooldown / PlayerStatsManager.Instance.attackSpeed.GetValue();;

            if (weapon.data.projectilePrefab != null)
            {
                foreach (Collider hit in hits)
                {
                    GameObject proj = Instantiate(weapon.data.projectilePrefab, playerObject.transform.position, Quaternion.identity);
                    proj.GetComponent<ProjectileVisual>().Launch(hit.transform);
                }
            }
        }
    }
}
