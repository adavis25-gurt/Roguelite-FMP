using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] GameObject playerObj;
    [SerializeField] float speed;

    bool paused;
    float timer = 1;

    private void Update()
    {
        float step = speed * Time.deltaTime;

        if(!paused )
        {
            transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position, step);
        }    
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("HIT THE PLAYER");
            paused = true;
            float timeStamp = Time.time + timer;
            while (paused)
            {
                if (timeStamp <= Time.time)
                {
                    paused = false;
                }
            }
        }
    }
}
