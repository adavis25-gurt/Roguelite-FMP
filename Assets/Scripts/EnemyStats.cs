using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] int health;

    void Die()
    {
        Destroy(gameObject);
        PlayerStatsManager.Instance.AddExp(20);
    }


    public void doDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
}
