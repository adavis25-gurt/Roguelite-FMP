using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    private GameObject playerObj;

    public void passPlayerIn(GameObject player)
    {
        if (!player) return;
        playerObj = player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerObj.SetActive(true);
            SceneManager.LoadScene("MainScene");
        }
    }
}
