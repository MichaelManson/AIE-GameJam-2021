using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print("hit" + collision.gameObject.name + ", " + collision.gameObject.layer + ", " + GameManager.roundWon);
        
        if (GameManager.roundWon) return;

        Player p = null;
        
        switch (collision.gameObject.layer)
        {
            case 8: // Player1
                p = PlayerManager.Instance.players[0];
                break;
            case 9: // Player2
                p = PlayerManager.Instance.players[1];
                break;
            case 10: // Player3
                p = PlayerManager.Instance.players[2];
                break;
            case 11: // Player4
                p = PlayerManager.Instance.players[3];
                break;
            default:
                return;
        }
        
        // Set this player to be 'dead'
        p.Dead = true;

        // Remove the dead player from the list of active players
        PlayerManager.Instance.activePlayers.Remove(p);
        
        // Set this player to be 'dead'
        p.Dead = true;

        // Remove the dead player from the list of active players
        PlayerManager.Instance.activePlayers.Remove(p);
        
        // Check if round has been won
        if (PlayerManager.Instance.activePlayers.Count != 1) return;
            
        // Tell the game that the last remaining player is the winner
        GameManager.Instance.MatchWinner(PlayerManager.Instance.activePlayers[0]);
        
        GameManager.Instance.RoundIsOver();
        
        GameManager.roundWon = true;
    }
}
