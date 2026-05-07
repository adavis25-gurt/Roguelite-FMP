using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponSlot
{
    public WeaponData data;
    [HideInInspector] public float cooldownTimer = 0f;
    [HideInInspector] public float currentDamage;
}

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject playerObject;
    public List<WeaponSlot> weapons;

    void Start()
    {
        foreach (WeaponSlot slot in weapons)
        {
            if (slot.data != null)
            {
                slot.currentDamage = (slot.data.baseDamage + PlayerStatsManager.Instance.attackDamage.GetValue()); 
            }
        }
    }

    void Attack(WeaponSlot slot, GameObject enemy)
    {
        EnemyStats stats = enemy.GetComponent<EnemyStats>();
        if (!stats) return;
        stats.TakeDamage(slot.currentDamage);
    }

    void Update()
    {
        foreach (WeaponSlot slot in weapons)
        {
            if (slot.data == null) continue;

            if (slot.cooldownTimer > 0f)
            {
                slot.cooldownTimer -= Time.deltaTime;
                continue;
            }

            Collider[] hits = Physics.OverlapSphere(
            playerObject.transform.position,
            slot.data.attackDistance,
            enemyLayer
            );

            foreach (Collider hit in hits)
            {
                Attack(slot, hit.transform.gameObject);
            }

            if (hits.Length > 0)
            {
                slot.cooldownTimer = slot.data.cooldown;
            }
        }
    }
}
