using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField] GameObject player;

    public static Loader Instance { get; private set; }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


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

    

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            player.SetActive(true);
            var spawner = GameObject.Find("Spawner");
            spawner.GetComponent<ObjectSpawner>().player = player.transform;
            var playerstatsmanager = GameObject.Find("PlayerStats");
            playerstatsmanager.GetComponent<PlayerStatsManager>().currentHealth = playerstatsmanager.GetComponent<PlayerStatsManager>().health.GetValue();
            playerstatsmanager.GetComponent<PlayerStatsManager>().increaseColor = GameObject.Find("RawImage").GetComponent<IncreaseColor>();
        }
        else if (scene.name == "TherapyRoom")
        {
            float colorAmount = GameObject.Find("BlackAndWhite").GetComponent<Renderer>().material.GetFloat("_ColorAmount");
            var greyscale = GameObject.Find("BlackAndWhite").GetComponent<Renderer>().material;
            greyscale.SetFloat("_ColorAmount", colorAmount += 0.17f);
        }

        print(scene.name);
    }
}

