using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOneController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool groundedPlayer;

    private Vector2 _movementInput = Vector2.zero;
    private bool _jumped = false;
    

    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _jumped = context.action.triggered;
    }

    void Update()
    {
        groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        // Horizontal input
        Vector3 move = new Vector3(_movementInput.x, 0, _movementInput.y);
        move = Vector3.ClampMagnitude(move, 1f); // Optional: prevents faster diagonal movement

        // facing/rotation with move dir
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        // Jump
        if (_jumped && groundedPlayer)
        {
            _playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        // Apply gravity
        _playerVelocity.y += gravityValue * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 finalMove = (move * playerSpeed) + (_playerVelocity.y * Vector3.up);
        _controller.Move(finalMove * Time.deltaTime);
    }
}
