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

    
    void Update()
    {
        if (ObjectSpawner.IsPaused()) return;

        seconds += Time.deltaTime;
        while (seconds >= 60)
        {
            seconds -= 60;
            minutes++;
        }
    }
}
