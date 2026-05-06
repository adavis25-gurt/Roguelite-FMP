using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public static Loader Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void EnablePlayer(GameObject Player)
    {
        if (SceneManager.GetActiveScene().name == "Main Scene")
        {
            Player.SetActive(true); 
        }
    }
}

