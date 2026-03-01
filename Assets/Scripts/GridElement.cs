using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour
{
    private Vector3Int coordinates;

    [SerializeField] GameObject cube;
    [SerializeField] GameObject slope;
    [SerializeField] GameObject bottomPart;
    [SerializeField] GameObject bottomPartBlockPrefab;

    private static readonly Dictionary<Vector2Int, float> directionToYaw = new()
    {
        {Vector2Int.up, 0f},
        {Vector2Int.right, 90f},
        {Vector2Int.down, 180f},
        {Vector2Int.left, 270f},
    };

    public Vector3Int Coordinates { get => coordinates; set => coordinates = value;}
    public bool IsSlope => slope.activeSelf;

    public void SetElevation(int elevation, float yPosition)
    {
        coordinates.y = elevation;
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
    }

    public void MakeSlope(Vector2Int direction)
    {
        cube.gameObject.SetActive(false);
        slope.gameObject.SetActive(true);
        slope.transform.localEulerAngles = new Vector3(0, directionToYaw[direction], 0);
    }

    public void ConfigureBottomPart(float verticalBlockSpacing)
    {
        for (int i = 0; i < coordinates.y; i++)
        {
            Instantiate(bottomPartBlockPrefab, transform.position + Vector3.down * (i + 1) * verticalBlockSpacing, Quaternion.identity, bottomPart.transform);
        }
    }
}
