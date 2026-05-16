using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] Image fillImage;
    [SerializeField] TMP_Text healthText;

    BossStats stats;
    float maxHealth;
    float lastHealth;
    float damageShown;
    float damageTimer;

    void Start()
    {
        stats = GetComponentInParent<BossStats>();
        maxHealth = stats.currentHealth;
        lastHealth = maxHealth;
    }

    void Update()
    {
        float current = stats.currentHealth;

        if (current < lastHealth)
        {
            damageShown = current - lastHealth;
            damageTimer = 2f;
        }
        lastHealth = current;

        fillImage.fillAmount = current / maxHealth;

        string text = $"{Mathf.RoundToInt(current)}/{Mathf.RoundToInt(maxHealth)}";
        if (damageTimer > 0f)
        {
            damageTimer -= Time.deltaTime;
            text += $" ({Mathf.RoundToInt(damageShown)})";
        }
        healthText.text = text;

        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180f, 0);
    }
}