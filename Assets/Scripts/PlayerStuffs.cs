using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStuffs : MonoBehaviour
{
    void Update()
    {
        float maxHP = PlayerStatsManager.Instance.health.GetValue();
        float currentHP = PlayerStatsManager.Instance.currentHealth;
        int coins = PlayerStatsManager.Instance.coins;

        GameObject.Find("HealthBarFill").GetComponent<Image>().fillAmount = currentHP / maxHP;
        GameObject.Find("CoinText").GetComponent<TMP_Text>().text = coins.ToString();
        GameObject.Find("HealthText").GetComponent<TMP_Text>().text = $"{Mathf.RoundToInt(currentHP)}/{Mathf.RoundToInt(maxHP)}";
    }
}