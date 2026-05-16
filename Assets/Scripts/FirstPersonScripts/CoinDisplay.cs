using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.text = PlayerStatsManager.Instance.coins.ToString();
    }
}