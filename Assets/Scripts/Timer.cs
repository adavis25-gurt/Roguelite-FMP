using UnityEngine;

public class Timer : MonoBehaviour
{
    public float seconds, minutes;
    void Update()
    {
        seconds += Time.deltaTime;
        while (seconds >= 60)
        {
            seconds -= 60;
            minutes++;
        }
    }
}
