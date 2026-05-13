using UnityEngine;
using UnityEngine.UIElements;

public class ShopUIController : MonoBehaviour
{
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private ItemData[] availableItems;
    [SerializeField] private int baseCost = 50;
    [SerializeField] PauseManager pauseManager;

    private VisualElement ui;
    private Label[] itemLabels = new Label[3];
    private ItemData[] currentItems = new ItemData[3];

    private void Awake() => ui = GetComponent<UIDocument>().rootVisualElement;

    private void OnEnable()
    {
        for (int i = 0; i < 3; i++)
        {
            var slot = ui.Q<VisualElement>($"Slot{i}");
            itemLabels[i] = slot.Q<Label>("ItemLabel");
            AssignRandom(i);

            int index = i;
            slot.Q<Button>("BuyButton").clicked += () =>
            {
                if (shopManager.BuyItem(currentItems[index], baseCost))
                    AssignRandom(index);
                else
                    itemLabels[index].text = "Not enough coins!";
            };
        }

        ui.Q<Button>("ExitButton").clicked += Close;
    }

    private void AssignRandom(int index)
    {
        currentItems[index] = availableItems[Random.Range(0, availableItems.Length)];
        itemLabels[index].text = $"{currentItems[index].itemName}\n{shopManager.GetCost(baseCost)} coins\n\n{currentItems[index].Description}";
    }

    public void Open()
    {
        Time.timeScale = 0;
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = false;
        ui.Q<VisualElement>("Panel").RemoveFromClassList("hide");
    } 
    public void Close()
    {
        Time.timeScale = 1;
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = true;
        ui.Q<VisualElement>("Panel").AddToClassList("hide");
    } 
}