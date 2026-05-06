using UnityEngine;
using Unity.Cinemachine;

public class CinemachineScript : MonoBehaviour
{
    [SerializeField] Transform lookAtTarget;
    
    void Update()
    {
        if (lookAtTarget == null)
        {
            lookAtTarget = GameObject.Find("PlayerObj").transform;
            return;
        }
        GetComponent<CinemachineCamera>().Follow = lookAtTarget;   
    }
}
