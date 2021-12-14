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
    public void KillPlayer()
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

            // death area
            //if (!GameManager.watchForDeath) return;

            //GameManager.watchForDeath = false;

            if (GameManager.roundWon) return;

            Player p = null;

            p = PlayerManager.Instance.CheckIfCollidedWithPlayer(this.gameObject.layer);

            if (!p) return;

            p.Dead = true;

            PlayerManager.Instance.activePlayers.Remove(p);

            //needs resetting?
            GameManager.watchForDeath = true;

            if (GameManager.Instance.CurrentLevelObjectiveType != LevelManager.LevelObjectiveType.Deathmatch) return;

            if (PlayerManager.Instance.activePlayers.Count != 1) return;

            Debug.Log(PlayerManager.Instance.activePlayers[0].name);

            GameManager.Instance.SetRoundWinner(PlayerManager.Instance.activePlayers[0]);

            Debug.Log(GameManager.Instance.RoundWinner.GetComponent<PlayerConfig>().PlayerId);

            GameManager.Instance.RoundWinner.Wins++;

            GameManager.Instance.RoundIsOver();

            GameManager.roundWon = true;


        }
        CameraShaker.Instance.ShakeOnce(2f, 2f, 0.1f, .75f);
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
