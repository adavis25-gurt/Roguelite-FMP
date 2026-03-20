using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance {get; private set;}

    [SerializeField] GameObject playerObj;

    public int level = 0;
    public float currentEXP = 0f;
    public float expRequired = 100f;

    public Stat health = new Stat(100f);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (health.GetValue() <= 0)
        {
            Die();
        }
    }

    public void AddExp(float amount)
    {
        currentEXP += (amount * CharacterStats.Instance.expMultiplier.GetValue());
        print(CharacterStats.Instance.expMultiplier.GetValue());
        print(CharacterStats.Instance.moveSpeed.GetValue());

        while (currentEXP >= expRequired)
        {
            currentEXP -= expRequired;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        health.baseValue += 50f;
        expRequired *= 1.05f;
    }

    void Die()
    {
        Destroy(playerObj);
    }

    public void doDamage(int amount)
    {
        if (amount >= health.GetValue())
        {
            Die();
        }
        health.GetValue() -= amount;
    }
}
