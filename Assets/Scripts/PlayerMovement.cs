using System.Xml;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float groundCheckRadius = 0.3f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject groundCheck;
    [SerializeField] Transform orientation;

    Rigidbody playerRigidbody;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        jumpAction = playerInput.actions.FindAction("Jump");

        jumpAction.performed += OnJump;
    }

    void OnDestroy()
    {
        jumpAction.performed -= OnJump;
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (!IsGrounded()) return;
        playerRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    bool IsGrounded()
    {
        return Physics.SphereCast(
            groundCheck.transform.position,
            groundCheckRadius,
            Vector3.down,
            out RaycastHit hit,
            groundCheckRadius,
            groundLayer
        );
    }

    void FixedUpdate()
    {
        MovePlayer();
        print(IsGrounded());
    }

    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 move = orientation.forward * direction.y + orientation.right * direction.x;
        move.y = 0;

        if (IsGrounded())
        {
            Vector3 newVelocity = move.normalized * moveSpeed;
            newVelocity.y = playerRigidbody.linearVelocity.y;
            playerRigidbody.linearVelocity = newVelocity;
        }
        else
        {
            playerRigidbody.AddForce(move.normalized * moveSpeed * 0.15f, ForceMode.VelocityChange);

            Vector3 horizontalVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0, playerRigidbody.linearVelocity.z);

            float airSpeedCap = moveSpeed * 0.40f;
            if (horizontalVelocity.magnitude > airSpeedCap)
            {
                Vector3 clampedVelocity = horizontalVelocity.normalized * airSpeedCap;
                playerRigidbody.linearVelocity = new Vector3(clampedVelocity.x, playerRigidbody.linearVelocity.y, clampedVelocity.z);
            }
        }
    }
}
