using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] ObjectSpawner ObjectSpawner;

    int lastMinute;

    public float seconds, minutes;
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
        }
    }
}
