using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public static CharacterStats Instance { get; private set; }

    public Stat health;
    public Stat attackDamage  = new Stat(10f);
    public Stat attackSpeed   = new Stat(1f);
    public Stat defense       = new Stat(5f);
    public Stat jumpPower     = new Stat(8f);
    public Stat moveSpeed     = new Stat(5f);
    public Stat jumpAmount    = new Stat(1f);
    public Stat expMultiplier = new Stat(1f);

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        health = new Stat(PlayerStatsManager.Instance.health.baseValue);
    }
}
