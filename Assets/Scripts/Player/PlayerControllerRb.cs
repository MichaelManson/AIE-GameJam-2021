using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRb : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    private float moveSpeed;

    [SerializeField] private float jumpForce = 1.0f;

    private Rigidbody rb;

    private Vector2 moveInput;
    private Vector3 velocity = Vector3.zero;

    private PlayerControls playerControls;

    private void Awake()
    {
        // Init input actions
        playerControls = new PlayerControls();

        rb = GetComponent<Rigidbody>();
        Debug.Assert(rb != null, "rb is null!");
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = playerControls.Player.Move.ReadValue<Vector2>() * moveSpeed;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed * Time.deltaTime;
    }
}
