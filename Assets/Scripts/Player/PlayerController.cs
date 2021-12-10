using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerConfig playerConfig;

    [SerializeField] private Animator anim;

    [Tooltip("What is the cap speed of player's horizontal speed")] [SerializeField] 
    private float maxSpeed = 5.0f;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float airSpeed = 1.0f;
    [SerializeField] private float jumpForce = 1.0f;

    [Tooltip("The amount of force applied on the character when they melee")]
    [SerializeField] private float meleeForce = 25.0f;

    public Rigidbody Hips { get; set; }
    [SerializeField] private Rigidbody hips;

    [Header("Combat")]
    public float damage = 5.0f;
    private float meleeTimer = 0.0f;
    [Tooltip("The amount of delays required between melee attacks")] [SerializeField] 
    private float meleeRate = 0.5f;
    public bool CanMelee { get; set; }

    public bool IsGrounded { get; set; }

    [SerializeField]
    private ConfigurableJoint hipJoint;

    private Vector3 velocity;

    private bool hasJumped;

    private Vector2 moveInput;

    private float currentSpeed;

    private void Start()
    {
        // Default isGrounded check to true
        IsGrounded = true;
        CanMelee = true;
        meleeTimer = meleeRate;

        Hips = hips;
    }

    private void Update()
    {
        // Update animation states
        anim.SetFloat("MoveX", Mathf.Abs(moveInput.x));
        anim.SetBool("Grounded", IsGrounded);

        meleeTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        velocity = new Vector3(moveInput.x, 0, 0).normalized;

        if (velocity.magnitude >= 0.1f && PlayerManager.CanMove)
        {
            Debug.Log("Applying force");
            Vector3 dir = -velocity;

            // Rotate the character
            float targetAngle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
            hipJoint.targetRotation = Quaternion.Euler(0, targetAngle, 0);

            // If the player is moving while in air...
            if (!IsGrounded)
            {
                Debug.Log("IN AIR");
                hips.AddForce(velocity * airSpeed);
            }
            else
            {
                Debug.Log("ON GROUND");
                hips.AddForce(velocity * speed);
            }
        }

        if(hasJumped && IsGrounded && PlayerManager.CanJump)
        {
            Debug.Log("Moving char up");

            hips.AddForce(Vector3.up * jumpForce);
            IsGrounded = false;

            hasJumped = false;
        }

        // Clamps player's horizotal speed
        float controlledXVelocity = Mathf.Clamp(hips.velocity.x, -maxSpeed, maxSpeed);
        Vector3 currentVelocity = hips.velocity;
        currentVelocity.x = controlledXVelocity;
        hips.velocity = currentVelocity;

        //Debug.Log("VECLOTIY: " + hips.velocity);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        hasJumped = context.action.triggered;
        anim.SetTrigger("Jump");
    }

    public void OnMelee(InputAction.CallbackContext context)
    {
        // If the player can melee
        if ((meleeTimer >= meleeRate))
        {
            meleeTimer = 0.0f;

            CanMelee = true;

            anim.SetTrigger("Melee");

            // Adds a force on melee
            hips.AddForce(hips.transform.right * meleeForce);
        }
    }
}
