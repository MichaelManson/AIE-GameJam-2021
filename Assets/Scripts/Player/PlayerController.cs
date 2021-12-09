using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float jumpForce = 1.0f;
    [SerializeField] private Rigidbody hips;
    public bool IsGrounded { get; set; }

    private PlayerControls playerControls;

    private Vector3 velocity;

    bool isJump;

    private void Awake()
    {
        hips = GetComponent<Rigidbody>();
        Debug.Assert(hips != null, "hips are null!");

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

        playerControls.Player.Jump.performed += Jump_performed;
        playerControls.Player.Jump.canceled += Jump_Notperformed;
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        //isJump = true;
    }

    private void Jump_Notperformed(InputAction.CallbackContext obj)
    {
        //isJump = false;
    }

    private void OnDisable()
    {
        playerControls.Enable();


        playerControls.Player.Jump.performed -= Jump_performed;
        playerControls.Player.Jump.canceled -= Jump_Notperformed;
    }

    private void Update()
    {
        if (playerControls.Player.Jump.triggered)
        {
            //if(IsGrounded)
            //{
            Debug.Log("Jumping");

            isJump = true;
        }
        //else
           // isJump = false;
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = playerControls.Player.Move.ReadValue<Vector2>();

        velocity = new Vector3(moveInput.x, 0, 0).normalized;

        if (velocity.magnitude >= 0.1f)
        {

            hips.AddForce(velocity * speed);
        }


        if(isJump && IsGrounded)
        {
            Debug.Log("Moving char up");
            //if(IsGrounded)
            //{
               // Debug.Log("Jumping");
                hips.AddForce(Vector3.up * jumpForce);
                IsGrounded = false;
            //}

            isJump = false;
        }
    }
}
