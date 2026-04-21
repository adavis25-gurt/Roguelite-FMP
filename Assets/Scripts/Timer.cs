using UnityEngine;

public class Timer : MonoBehaviour
{
    public float seconds, minutes;

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemies;

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
