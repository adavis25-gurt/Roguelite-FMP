using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance {get; private set;}

    [SerializeField] GameObject playerObj;

    public Stat health = new Stat(100f);
    public Stat attackDamage = new Stat(10f);
    public Stat attackSpeed = new Stat(1f);
    public Stat defense = new Stat(5f);
    public Stat jumpPower = new Stat(8f);
    public Stat moveSpeed = new Stat(16f);
    public Stat jumpAmount = new Stat(1f);
    public Stat expMultiplier = new Stat(1f);
    public Stat CritChance = new Stat(2.5f);
    public Stat CritDamage = new Stat(50f);
    public Stat projectileCount = new Stat(1f);

    public float currentHealth;

    public bool canTakeDamage = true;
    public int coins;

    public int timesInTherapyRoom = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currentHealth = health.GetValue();
    }

    public void RestoreHealth()
    {
        currentHealth = health.GetValue();
    }

    public void ApplyItem(ItemData data)
    {
        StatModifier modifier = new StatModifier(data.value, data.modifierType);

        switch (data.statType)
        {
            case StatType.Health:
                health.AddModifier(modifier);
                currentHealth += data.value;
                break;
            case StatType.AttackDamage:
                attackDamage.AddModifier(modifier);
                break;
            case StatType.AttackSpeed:
                attackSpeed.AddModifier(modifier);
                break;
            case StatType.Defense:
                defense.AddModifier(modifier);
                break;
            case StatType.JumpPower:
                jumpPower.AddModifier(modifier);
                break;
            case StatType.MoveSpeed:
                moveSpeed.AddModifier(modifier);
                break;
            case StatType.JumpAmount:
                jumpAmount.AddModifier(modifier);
                break;
            case StatType.ExpMultiplier:
                expMultiplier.AddModifier(modifier);
                break;
            case StatType.CritChance:
                CritChance.AddModifier(modifier);
                break;
            case StatType.CritDamage:
                CritDamage.AddModifier(modifier);
                break;
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerObj.gameObject.SetActive(false);
        SceneManager.LoadSceneAsync("TherapyRoom");
        timesInTherapyRoom += 1;
    }

    public void doDamage(int amount)
    {
        if (!canTakeDamage) return;
        float reduced = Mathf.Max(1, amount - PlayerStatsManager.Instance.defense.GetValue());
        currentHealth -= reduced;
        if (currentHealth <= 0) Die();
    }

    public IEnumerator iFrameLogic()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = true;
    }
}
