using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerScript : MonoBehaviour
{
        public float speed = 5f;
        public float jump = 2f;
        public float gravity = -9.81f;
        public PlayerInput input;
        private InputAction _moveAction, _jumpAction;
        
        private CharacterController cc;
        private Vector3 velocity;

        void Start()
        {
            cc = GetComponent<CharacterController>();
            input = GetComponent<PlayerInput>();
            _jumpAction = input.actions.FindAction("Jump");
            _moveAction = input.actions.FindAction("Move");
        }

        void Update()
        {
            Vector2 actionMovement = _moveAction.ReadValue<Vector2>();
            Vector3 move = new Vector3(actionMovement.x, 0, actionMovement.y);
            move = transform.TransformDirection(move);
            cc.Move(move * speed * Time.deltaTime);
        }
    }
