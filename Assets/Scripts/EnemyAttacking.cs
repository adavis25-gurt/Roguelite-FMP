using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] GameObject playerObj;

    Animator animator;
    EnemyStats stats;
    bool paused = false;

    void Awake()
    {
        playerObj = GameObject.Find("Player");
        stats = GetComponent<EnemyStats>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!paused)
        {
            float step = stats.speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, (playerObj.transform.position + new Vector3(0, 2f, 0)), step);
            transform.LookAt(playerObj.transform.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (paused) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStatsManager.Instance.doDamage((int)stats.damage);
            StartCoroutine(PlayerStatsManager.Instance.iFrameLogic());
            StartCoroutine(CooldownLogic());
        }
    }

    IEnumerator CooldownLogic()
    {
        paused = true;
        yield return new WaitForSeconds(1f);
        paused = false;
    }

    public void SetTarget(GameObject target)
    {
        playerObj = target;
    }
}
