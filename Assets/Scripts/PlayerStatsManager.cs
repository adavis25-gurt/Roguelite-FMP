using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    bool canTakeDamage = true;
    public int coins;

    [SerializeField] Teleporter teleporter;
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerObj.gameObject.SetActive(false);
        SceneManager.LoadScene("TherapyRoom");
        teleporter.passPlayerIn(playerObj);
    }

    public void doDamage(int amount)
    {
        if (!canTakeDamage) return;
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

    public IEnumerator iFrameLogic()
    {
        canTakeDamage = false;
        print("cant take damage");
        yield return new WaitForSeconds(0.5f);
        print("can take damage");
        canTakeDamage = true;
    }
}
