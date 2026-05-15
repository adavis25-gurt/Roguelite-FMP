using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField] GameObject player;

    public Material greyscaleMain;
    public Material greyscaleTherapyRoom;

    float color = 0;
    public GameObject bossPrefab;

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
            playerstatsmanager.GetComponent<PlayerStatsManager>().canTakeDamage = true;
            StartCoroutine(LateStart(0.1f));
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

    IEnumerator LateStart(float waitForSeconds)
    {
        yield return new WaitForSeconds(waitForSeconds);
        player.transform.gameObject.SetActive(false);
        var terrainGen = GameObject.Find("TerrainGenerator");
        var TerrainGeneratorScript = terrainGen.GetComponent<TerrainGenerator>();
        Vector3 Position = (TerrainGeneratorScript.GetGridElement(TerrainGeneratorScript.mapSize / 2, TerrainGeneratorScript.mapSize / 2).transform.position + Vector3.up * 10f);
        print(TerrainGeneratorScript.GetGridElement(TerrainGeneratorScript.mapSize / 2, TerrainGeneratorScript.mapSize / 2));
        player.transform.position = Position;
        player.transform.gameObject.SetActive(true);

        if (PlayerStatsManager.Instance.timesInTherapyRoom >= 6)
        {
            var spawner = GameObject.Find("Spawner");
            spawner.SetActive(false);
            Instantiate(bossPrefab, Position + (Vector3.right * 5), Quaternion.identity);
        }
    }

}

