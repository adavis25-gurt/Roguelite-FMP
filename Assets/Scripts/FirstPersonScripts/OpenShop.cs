using UnityEngine;
using UnityEngine.UIElements;

public class OpenShop : MonoBehaviour, IInteractable
{
    [SerializeField] private ShopUIController shopUI;

    public string GetDescription()
    {
        return "Open Shop";
    }

    public void Interact()
    {
        shopUI.Open();
    }
}