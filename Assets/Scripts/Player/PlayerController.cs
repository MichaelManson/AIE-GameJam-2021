using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerConfig playerConfig;

    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float airSpeed = 1.0f;
    [SerializeField] private float jumpForce = 1.0f;

    public Rigidbody Hips { get; set; }
    [SerializeField] private Rigidbody hips;

    [SerializeField] private GameObject meleeCollision;
    public bool IsGrounded { get; set; }

    [SerializeField]
    private ConfigurableJoint hipJoint;

    private PlayerControls playerControls;

    private Vector3 velocity;

    private bool hasJumped;

    private Vector2 moveInput;

    private float currentSpeed;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        // Default isGrounded check to true
        IsGrounded = true;

        Hips = hips;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        return;
        if (playerControls.Player.Jump.triggered)
        {
            Debug.Log("Jumping");

            hasJumped = true;
        }
        
        if(playerControls.Player.Punch.triggered)
        {
            Punch();
        }

        moveInput = playerControls.Player.Move.ReadValue<Vector2>();
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

                //currentSpeed = airSpeed;
            }
            else
            {
                Debug.Log("ON GROUND");
                hips.AddForce(velocity * speed);

                //currentSpeed = speed;
            }
        }


        if(hasJumped && IsGrounded && PlayerManager.CanJump)
        {
            Debug.Log("Moving char up");

            hips.AddForce(Vector3.up * jumpForce);
            IsGrounded = false;

            hasJumped = false;

            //velocity.y += jumpForce;

        }
        //hips.velocity = velocity * currentSpeed * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        hasJumped = context.action.triggered;
    }

    public void OnMelee(InputAction.CallbackContext context)
    {
        StartCoroutine(ActiveMeleeCollision());
    }

    /// <summary>
    /// Will be activated and deactived via animation event
    /// </summary>
    private void Punch()
    {
        StartCoroutine(ActiveMeleeCollision());
    }

    /// <summary>
    /// Enable the melee box for a short time,
    /// then disable
    /// </summary>
    /// <returns></returns>
    private IEnumerator ActiveMeleeCollision()
    {
        meleeCollision.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        meleeCollision.SetActive(false);
    }
}
