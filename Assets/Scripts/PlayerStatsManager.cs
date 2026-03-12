using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance {get; private set;}

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

    public void AddExp(float amount)
    {
        currentEXP += amount;

        if (currentEXP >= expRequired)
        {
            currentEXP -= expRequired;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        health.baseValue += 50f;
        expRequired *= 1.15f;
    }
}
