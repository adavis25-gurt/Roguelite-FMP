using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] ObjectSpawner ObjectSpawner;

    int lastMinute;

    public float seconds, minutes;

    [SerializeField] GameObject player;
    [SerializeField] Transform shopSpawn;
    [SerializeField] GameObject enemies;

    Vector3 playerLastPos;

    void TeleportToShop()
    {
        foreach (Transform child in enemies.transform)
        {
            Destroy(child.gameObject);
        }
        playerLastPos = player.transform.position;
        player.transform.position = shopSpawn.position;
        print("yea bro teleported apparently");
        //TeleportBack();
        //ObjectSpawner.ToggleSpawning();
    }

    void TeleportBack()
    {
        player.transform.position = playerLastPos;
    }

    void Update()
    {
        if (ObjectSpawner.IsPaused()) return;

        seconds += Time.deltaTime;
        while (seconds >= 60)
        {
            seconds -= 60;
            minutes++;
        }

        if (minutes > 0 && minutes != lastMinute){
            lastMinute = (int)minutes;
            ObjectSpawner.ToggleSpawning();
            TeleportToShop();
        }
    }
}
