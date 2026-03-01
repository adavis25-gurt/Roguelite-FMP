using System.Xml;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        Debug.Log("OnJump called");
        Debug.Log("Grounded: " + IsGrounded());

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

    void OnDrawGizmos()
{
    if (groundCheck == null) return;

    // Change color based on whether we're grounded or not
    // Green = grounded, Red = not grounded
    Gizmos.color = IsGrounded() ? Color.green : Color.red;

    // Draw a wire sphere at the start of the cast (where it begins)
    Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);

    // Draw a wire sphere at the end of the cast (how far down it reaches)
    // This shows you the full range the SphereCast sweeps through
    Gizmos.DrawWireSphere(groundCheck.transform.position + Vector3.down * groundCheckRadius, groundCheckRadius);

    // Draw a line connecting them so you can see the cast direction clearly
    Gizmos.DrawLine(groundCheck.transform.position, groundCheck.transform.position + Vector3.down * groundCheckRadius);
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
