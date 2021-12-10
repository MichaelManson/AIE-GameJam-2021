using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        print("hit" + other.gameObject.name + ", " + other.gameObject.layer + ", " + GameManager.roundWon);
        
        if (GameManager.roundWon) return;

        Player p = null;

        p = PlayerManager.Instance.CheckIfCollidedWithPlayer(other.gameObject.layer);

        if (!p) return;
        
        // Set this player to be 'dead'
        p.Dead = true;

        // Remove the dead player from the list of active players
        PlayerManager.Instance.activePlayers.Remove(p);
        
        // Set this player to be invisible
        //p.gameObject.SetActive(false);

        // Remove the dead player from the list of active players
        //PlayerManager.Instance.activePlayers.Remove(p);
        
        // First, see if the level is a deathmatch type
        if (GameManager.Instance.CurrentLevelObjectiveType != LevelManager.LevelObjectiveType.Deathmatch) return;
        
        // Check if round has been won
        if (PlayerManager.Instance.activePlayers.Count != 1) return;

        Debug.Log(PlayerManager.Instance.activePlayers[0].name);
        // Tell the game that the last remaining player is the winner
        GameManager.Instance.SetRoundWinner(PlayerManager.Instance.activePlayers[0]);

        Debug.Log(GameManager.Instance.RoundWinner.GetComponent<PlayerConfig>().PlayerId);

        GameManager.Instance.RoundWinner.Wins++;
        
        GameManager.Instance.RoundIsOver();
        
        GameManager.roundWon = true;
    }
}
