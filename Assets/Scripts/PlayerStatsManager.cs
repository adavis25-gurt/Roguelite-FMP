using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance {get; private set;}

    [SerializeField] GameObject playerObj;

    public int level = 0;
    public float currentEXP = 0f;
    public float expRequired = 100f;

    public Stat health = new Stat(100f);

    public float currentHealth;

    PlayerMovement playerMove;

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

    private void Start()
    {
        currentHealth = CharacterStats.Instance.health.GetValue();
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
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void doDamage(int amount)
    {
        if (amount >= currentHealth)
        {
            Die();
        }
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            print("DEAD");
            Die();
        }
    }
}
