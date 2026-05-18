using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialMovemenet : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] float groundCheckRadius = 0.3f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject groundCheck;
    [SerializeField] Transform orientation;

    [Header("Fall Gravity")]
    [SerializeField] float fallMultiplier = 3f;
    [SerializeField] float airControlMultiplier = 0.1f;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;
    Rigidbody playerRigidbody;
    Animator animator;

    int jumpsRemaining;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        animator = GameObject.Find("Kendrarig").GetComponent<Animator>();
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
        if (jumpsRemaining <= 0) return;
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
        playerRigidbody.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        jumpsRemaining--;
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
        HandleGrounding();
        MovePlayer();
        ApplyFallGravity();
    }

    void HandleGrounding()
    {
        if (IsGrounded())
        {
            jumpsRemaining = 1;
        }
    }

    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 move = orientation.forward * direction.y + orientation.right * direction.x;
        move.y = 0;

        if (IsGrounded())
        {
            Vector3 newVelocity = move.normalized * 10f;
            newVelocity.y = playerRigidbody.linearVelocity.y;
            playerRigidbody.linearVelocity = newVelocity;
            animator.SetBool("IsMoving", true);
        }
        else
        {
            Vector3 horizontalVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
            float airSpeedCap =  airControlMultiplier;

            if (horizontalVelocity.magnitude < airSpeedCap)
            {
                playerRigidbody.AddForce(move.normalized * 5f * airControlMultiplier, ForceMode.VelocityChange);
            }
        }

        if (move == Vector3.zero)
        {
            animator.SetBool("IsMoving", false);
        }
    }

    void ApplyFallGravity()
    {
        if (playerRigidbody.linearVelocity.y < 0)
        {
            playerRigidbody.AddForce(Vector3.down * fallMultiplier, ForceMode.Acceleration);
        }
    }

}
