using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField] GameObject player;

    public Material greyscaleMain;
    public Material greyscaleTherapyRoom;

    float color = 0;

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
            greyscaleMain = GameObject.Find("RawImage").GetComponent<RawImage>().material;
            greyscaleMain.SetFloat("_ColorAmount", color);
            print(greyscaleMain.GetFloat("_ColorAmount"));
            playerstatsmanager.GetComponent<PlayerStatsManager>().canTakeDamage = true;

        }
        else if (scene.name == "TherapyRoom")
        {
            color += 0.17f;
            greyscaleTherapyRoom = GameObject.Find("BlackAndWhite").GetComponent<RawImage>().material;
            greyscaleTherapyRoom.SetFloat("_ColorAmount", color);
            print(greyscaleMain.GetFloat("_ColorAmount"));
        }

        print(scene.name);
    }
}

