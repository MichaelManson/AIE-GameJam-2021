using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print("hit" + collision.gameObject.name);

        if (!collision.gameObject.TryGetComponent(out Player p)) return;
        
        if (GameManager.roundWon) return;
            
        // Set this player to be 'dead'
        p.Dead = true;

        // Remove the dead player from the list of active players
        PlayerManager.Instance.activePlayers.Remove(p);
            
        // Turn the gameObject off
        gameObject.SetActive(false);
            
        // Check if round has been won
        if (PlayerManager.Instance.activePlayers.Count != 1) return;
            
        // Tell the game that the last remaining player is the winner
        GameManager.Instance.MatchWinner(PlayerManager.Instance.activePlayers[0]);
        GameManager.roundWon = true;
    }
}
