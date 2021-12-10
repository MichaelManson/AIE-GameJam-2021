using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to the animated version of the character,
/// this script ensures animation events are performed
/// </summary>
public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private MeleeCollision meleeCollision;

    [SerializeField] private GameObject cloudParticleEffect;
    //[SerializeField] private ParticleSystem cloudParticleEffectP;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        Debug.Assert(playerController != null, "playerControler is null!");
    }

    /// <summary>
    /// Unity animation event. 
    /// Enables the melee box
    /// </summary>   
    public void StartAttack()
    {
        if (playerController.CanMelee)
        {
            playerController.CanMelee = false;

            Debug.Log("Start attack");
            meleeCollision.gameObject.SetActive(true);

            cloudParticleEffect.SetActive(true);
            //cloudParticleEffectP.Play();
        }
    }

    /// <summary>
    /// Unity animation event. 
    /// Disables the melee box
    /// </summary>
    public void EndAttack()
    {
        Debug.Log("End attack");
        meleeCollision.gameObject.SetActive(false);

        // Reset hit
        meleeCollision.HasHit = false;

        cloudParticleEffect.SetActive(false);
    }
}
