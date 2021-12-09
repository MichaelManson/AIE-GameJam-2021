using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float jumpForce = 1.0f;
    [SerializeField] private Rigidbody hips;

    [SerializeField] private BoxCollider meleeCollision;
    public bool IsGrounded { get; set; }

    private float turnSmoothVelocity;
    [SerializeField] private float turnSmoothTime = 0.1f;

    [SerializeField]
    private ConfigurableJoint hipJoint;

    private PlayerControls playerControls;

    private Vector3 velocity;

    bool isJump;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        // Default isGrounded check to true
        IsGrounded = true;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        if (playerControls.Player.Jump.triggered)
        {
            Debug.Log("Jumping");

            isJump = true;
        }
        
        if(playerControls.Player.Punch.triggered)
        {
            Punch();
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = playerControls.Player.Move.ReadValue<Vector2>();

        Debug.Log(moveInput);

        velocity = new Vector3(moveInput.x, 0, 0).normalized;

        if (velocity.magnitude >= 0.1f)
        {
            Debug.Log("Applying force");
            Vector3 dir = -velocity;

            // Rotate the character
            float targetAngle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
            hipJoint.targetRotation = Quaternion.Euler(0, targetAngle, 0);

            hips.AddForce(velocity * speed);
        }


        if(isJump && IsGrounded)
        {
            Debug.Log("Moving char up");

            hips.AddForce(Vector3.up * jumpForce);
            IsGrounded = false;

            isJump = false;
        }
    }

    /// <summary>
    /// Will be activated and deactived via animation event
    /// </summary>
    private void Punch()
    {
        meleeCollision.enabled = true;
    }
}
