using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;

    [Tooltip("Used as a temporary death animation")]
    [SerializeField] private ConfigurableJoint hipJoint;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        Debug.Assert(playerController != null, "playerController is null!");
    }

    [ContextMenu("Kill player")]
    private void KillPlayer()
    {
        TakeDamage(100.0f);
    }

    public void TakeDamage(float damage)
    {
        // Subtract the damage
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("Player is dead");

            // Disable player movement, so they can't move
            playerController.enabled = false;

            // Make the player fall over
            JointDrive jointDrive = hipJoint.angularXDrive;
            jointDrive.positionSpring = 0f;
            hipJoint.angularXDrive = jointDrive;


            JointDrive jointDriveYZ = hipJoint.angularYZDrive;
            jointDriveYZ.positionSpring = 0f;
            hipJoint.angularYZDrive = jointDriveYZ;
        }
    }

}
