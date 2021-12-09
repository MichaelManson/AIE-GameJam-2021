using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if the player is grounded regardless if they fall
/// on their head, torso, feet etc...
/// </summary>
public class GroundCheck : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private void OnCollisionEnter(Collision collision)
    {
        playerController.IsGrounded = true;
    }
}
