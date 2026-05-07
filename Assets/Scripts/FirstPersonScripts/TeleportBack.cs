using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class TeleportBack : MonoBehaviour, IInteractable
{
    public string GetDescription()
    {
        return "Teleport back to the main game!";
    }

    public void Interact()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }
}