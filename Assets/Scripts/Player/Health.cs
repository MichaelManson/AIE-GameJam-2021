using EZCameraShake;
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
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, .75f);
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

    public void Revive()
    {
        health = maxHealth;

        // Enable player movement, so they can move
        playerController.enabled = true;

        // Make the player balance
        JointDrive jointDrive = hipJoint.angularXDrive;
        jointDrive.positionSpring = 750f;
        hipJoint.angularXDrive = jointDrive;

        JointDrive jointDriveYZ = hipJoint.angularYZDrive;
        jointDriveYZ.positionSpring = 750f;
        hipJoint.angularYZDrive = jointDriveYZ;
    }

}
