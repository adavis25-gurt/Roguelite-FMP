using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] float priceExponent = 1.15f;
    int purchaseCount = 0;

    public int GetCost(int baseCost)
    {
        return Mathf.RoundToInt(baseCost * Mathf.Pow(priceExponent, purchaseCount));
    }

    public bool BuyItem(ItemData data, int baseCost)
    {
        int cost = GetCost(baseCost);
        if (PlayerStatsManager.Instance.coins < cost) return false;
        PlayerStatsManager.Instance.coins -= cost;
        purchaseCount++;
        PlayerStatsManager.Instance.ApplyItem(data);
        return true;
    }
}
