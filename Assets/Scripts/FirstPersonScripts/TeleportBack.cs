using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportBack : MonoBehaviour, IInteractable
{
    public string GetDescription()
    {
        return "Teleport back to the main game!";
    }

    public void Interact()
    {
        SceneManager.LoadScene("MainScene");
    }
}