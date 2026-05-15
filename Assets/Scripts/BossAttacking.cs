using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossAttacking : MonoBehaviour
{
    [SerializeField] GameObject playerObj;

    NavMeshAgent agent;
    BossStats stats;
    bool paused = false;

    void Awake()
    {
        playerObj = GameObject.Find("Player");
        stats = GetComponent<BossStats>();
        agent = GetComponent<NavMeshAgent>();
        this.transform.position = playerObj.transform.position;
    }

    void Update()
    {
        if (!paused && agent.isOnNavMesh)
        {
            agent.speed = stats.speed;
            agent.SetDestination(playerObj.transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (paused) return;

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatsManager.Instance.doDamage((int)stats.damage);
            StartCoroutine(PlayerStatsManager.Instance.iFrameLogic());
            StartCoroutine(KnockbackThenCooldown());
        }
    }

    IEnumerator KnockbackThenCooldown()
    {
        paused = true;
        agent.ResetPath();

        Vector3 pushDir = (transform.position - playerObj.transform.position);
        pushDir.y = 0f;
        pushDir = pushDir.normalized;

        Vector3 knockbackTarget = transform.position + pushDir * 4f;

        while (Vector3.Distance(transform.position, knockbackTarget) > 0.05f)
        {
            agent.Move(pushDir * 8f * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.8f);
        paused = false;
    }

    public void SetTarget(GameObject target)
    {
        playerObj = target;
    }
}