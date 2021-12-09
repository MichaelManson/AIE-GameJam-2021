using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] [Range(0, 100)]
    private float moveSpeed;

    [SerializeField] private float jumpForce = 1.0f;

    [SerializeField] private float gravity = -20.0f;

    private CharacterController characterController;

    private Vector2 moveInput;
    private Vector3 velocity = Vector3.zero;

    private PlayerControls playerControls;

    bool isGrounded;

    private void Awake()
    {
        // Init input actions
        playerControls = new PlayerControls();

        characterController = GetComponent<CharacterController>();
        Debug.Assert(characterController != null, "characterController is null!");
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        moveInput = playerControls.Player.Move.ReadValue<Vector2>() * moveSpeed;

        if (moveInput.magnitude > 1)
        {
            // Ensures the magnitude of writeInput doesn't go over 1,
            // especially on a controller, where acceleration is used
            //moveInput = Vector3.ClampMagnitude(moveInput, 1f);
        }

        Debug.Log("Grounded: " + isGrounded);


        // Read jump
        if (playerControls.Player.Jump.triggered /*&& isGrounded*/)
        {
            Debug.Log("Jumping");
            velocity.y += Mathf.Sqrt(jumpForce * -3.0f * Physics.gravity.y);
        }

        if (isGrounded && velocity.y < 0)
            velocity.y = 0.0f;

        // If we are receiving input...
        // if (moveInput.magnitude >= 0.1f)
        //{
        // Calculate the velocity
        velocity.x = moveInput.x;
        velocity.z = 0.0f;

        // Apply gravity
        velocity.y += Physics.gravity.y * Time.deltaTime;

        Debug.Log("Velcoity: " + velocity);

        Debug.Log("Gravity: " + Physics.gravity.y);

        characterController.Move(velocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        isGrounded = true;
    }
}
