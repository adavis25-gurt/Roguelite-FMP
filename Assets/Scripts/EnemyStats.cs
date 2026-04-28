using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] float baseHealth = 30f;
    [SerializeField] float baseXPReward = 20f;
    [SerializeField] int baseCoinReward = 50;
    [SerializeField] float baseDamage = 20f;
    [SerializeField] float baseSpeed = 3f;

    [SerializeField] float healthGrowthRate = 0.3f;
    [SerializeField] float damageGrowthRate = 0.3f;
    [SerializeField] float speedGrowthRate = 0.8f;

    public Timer timer;

    float currentHealth;

    public float damage { get; private set; }
    public float speed { get; private set; }

    void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        float t = timer.minutes + (timer.seconds / 60f);
        currentHealth = baseHealth * (1f + t * healthGrowthRate);
    }

    void Update()
    {
        float t = timer.minutes + (timer.seconds / 60f);
        damage = baseDamage * (1f + t * damageGrowthRate);
        speed  = baseSpeed  * (1f + t * speedGrowthRate);
    }

    void Die()
    {
        int coins = baseCoinReward;
        PlayerStatsManager.Instance.AddCoins(coins);
        Destroy(gameObject);
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0f)
            Die();
    }
}