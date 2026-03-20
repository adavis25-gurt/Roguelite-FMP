using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] GameObject playerObj;
    [SerializeField] float speed;

    bool paused = false;
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
        if (paused) return;
        if (collision.gameObject.tag == "Player")
        {
            PlayerStatsManager.Instance.doDamage(20);
            print(PlayerStatsManager.Instance.health.GetValue());
            StartCoroutine(CooldownLogic());
        }
    }

    IEnumerator CooldownLogic()
    {
        paused = true;
        yield return new WaitForSeconds(1f);
        paused = false;
    }
}
