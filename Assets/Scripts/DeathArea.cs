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
        Debug.Log("Before layer check");


        p = PlayerManager.Instance.CheckIfCollidedWithPlayer(collision.gameObject.layer);

        if (!p) return;

        // Set this player to be 'dead'
        p.Dead = true;

        // Remove the dead player from the list of active players
        PlayerManager.Instance.activePlayers.Remove(p);

        // Set this player to be 'dead'
        //p.Dead = true;

        // Remove the dead player from the list of active players
        //PlayerManager.Instance.activePlayers.Remove(p);


        // First, see if the level is a deathmatch type
        //if (GameManager.Instance.CurrentLevelObjectiveType != LevelManager.LevelObjectiveType.Deathmatch) return;

        // Check if round has been won
        if (PlayerManager.Instance.activePlayers.Count != 1) return;

        // If everyone dies at the same time...
        else if (PlayerManager.Instance.activePlayers.Count == 0)
        {
            GameManager.Instance.RoundIsOver();

            GameManager.roundWon = true;

            return;
        }

        // Tell the game that the last remaining player is the winner
        GameManager.Instance.SetRoundWinner(PlayerManager.Instance.activePlayers[0]);

        GameManager.Instance.RoundIsOver();

        GameManager.roundWon = true;
    }
}
