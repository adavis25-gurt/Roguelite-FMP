using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject playerObject;

    float cooldown = 0;

    void Attack(GameObject enemy)
    {
        EnemyStats stats = enemy.GetComponent<EnemyStats>();
        if (!stats) return;

        stats.doDamage(20);
    }

    void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(playerObject.transform.position, 10f, Vector3.forward, 500f, enemyLayer, QueryTriggerInteraction.UseGlobal);
        foreach (RaycastHit hit in hits)
        {
            if (cooldown != 0)
                cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                print("yea done hitting after cooldown");
                Attack(hit.transform.gameObject);
                cooldown = 5;
            }
        }
    }
}
