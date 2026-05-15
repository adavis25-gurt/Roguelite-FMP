using UnityEngine;

public class BossStats : MonoBehaviour
{
    public float currentHealth = 1000f;
    public float damage = 50f;
    public float speed = 15f;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f) Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
