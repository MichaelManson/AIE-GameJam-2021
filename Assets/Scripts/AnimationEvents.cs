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

    /// <summary>
    /// Unity animation event. 
    /// Enables the melee box
    /// </summary>   
    public void StartAttack()
    {
        Debug.Log("Start attack");
        meleeCollision.gameObject.SetActive(true);
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
    }
}
