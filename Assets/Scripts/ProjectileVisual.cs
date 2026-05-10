using UnityEngine;

public class ProjectileVisual : MonoBehaviour
{
    private Transform target;
    private float speed = 15f;

    public void Launch(Transform targetTransform)
    {
        target = targetTransform;
    }

    private void Update()
    {
        if (target == null) { Destroy(gameObject); return; }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.LookAt(target.position);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
            Destroy(gameObject);
    }
}