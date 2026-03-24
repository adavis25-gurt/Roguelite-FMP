using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] float health;
    public Timer timer;

    private void Update()
    {
        if (timer.minutes >= 2)
        {
            health += (1.015f * Time.deltaTime);
        }
    }

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
