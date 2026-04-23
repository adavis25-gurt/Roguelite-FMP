using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerObj;
    [SerializeField] Rigidbody rb;
    [SerializeField] float rotationSpeed;
    

    PlayerInput playerInput;
    InputAction moveAction;

    void Awake()
    {
        playerInput = player.GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        if (orientation == null || player == null || playerObj == null || rb == null)
        {
            orientation = GameObject.Find("Orientation").transform;
            player = GameObject.Find("Player").transform;
            playerObj = GameObject.Find("PlayerObj").transform;
            rb = player.GetComponent<Rigidbody>();
        }


        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        Vector2 input = moveAction.ReadValue<Vector2>();

        Vector3 inputDir = orientation.forward * input.y + orientation.right * input.x;

        if (inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}