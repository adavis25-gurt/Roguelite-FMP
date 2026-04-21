using UnityEngine;

public class TerrainObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] treePrefabs;
    [SerializeField] private GameObject[] stonePrefabs;

    public void SpawnRandomObject(GridElement gridElement)
    {
        float roll = Random.Range(0f, 1f);

        if (roll > 0.75f)
        {
            SpawnObject(gridElement, treePrefabs, 0.25f);
        }
        else if (roll > 0.5f)
        {
            SpawnObject(gridElement, stonePrefabs, Random.Range(10f,20f));
        }
    }

    private void SpawnObject(GridElement gridElement, GameObject[] prefabs, float scale)
    {
        Vector3 position = new Vector3(Random.Range(-0.25f, 0.25f), 0.25f, Random.Range(-0.25f, 0.25f));
        Vector3 rotation = new Vector3(0f, Random.Range(0f, 360f), 0f);

        GameObject obj = Instantiate(
            prefabs[Random.Range(0, prefabs.Length)],
            gridElement.transform.position,
            Quaternion.identity,
            gridElement.transform
        );

        obj.transform.localScale = Vector3.one * scale;
        obj.transform.localPosition = position;
        obj.transform.localEulerAngles = rotation;
    }
}
