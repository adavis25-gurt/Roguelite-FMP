using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BossAttacking : MonoBehaviour
{
    [SerializeField] GameObject playerObj;

    Animator animator;
    BossStats stats;
    bool paused = false;

    void Awake()
    {
        playerObj = GameObject.Find("Player");
        stats = GetComponent<BossStats>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!paused)
        {
            float rand = Random.Range(0, 1f);
            float step = stats.speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, (playerObj.transform.position + new Vector3(rand, 1f, rand)), step);
            transform.LookAt(playerObj.transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (paused) return;
        if (other.gameObject.CompareTag("Player"))
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
