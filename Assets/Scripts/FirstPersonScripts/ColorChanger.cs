using UnityEngine;

public class ColorChanger : MonoBehaviour, IInteractable
{
    Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    public string GetDescription()
    {
        return "Change to a random colour";
    }

    public void Interact()
    {
        material.color = new Color(Random.value, Random.value, Random.value, Random.value);
    }
}
