using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] float baseHealth = 30f;
    [SerializeField] float baseXPReward = 20f;
    [SerializeField] float baseDamage = 20f;
    [SerializeField] float baseSpeed = 3f;

    [SerializeField] float healthGrowthRate = 0.3f;
    [SerializeField] float xpGrowthRate = 0.2f;
    [SerializeField] float damageGrowthRate = 0.3f;
    [SerializeField] float speedGrowthRate = 0.8f;

    public Timer timer;

    float currentHealth;

    public float damage { get; private set; }
    public float speed { get; private set; }

    void Start()
    {
        currentHealth = baseHealth;
    }

    void Update()
    {
        float t = timer.minutes + (timer.seconds / 60f);
        damage = baseDamage * (1f + t * damageGrowthRate);
        speed  = baseSpeed  * (1f + t * speedGrowthRate);
        print(damage);
        print(currentHealth);
    }

    void Die()
    {
        float t = timer.minutes + (timer.seconds / 60f);
        float xp = baseXPReward * (1f + t * xpGrowthRate);
        PlayerStatsManager.Instance.AddExp(xp);
        Destroy(gameObject);
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0f)
            Die();
    }
}
