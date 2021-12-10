using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable once MemberCanBePrivate.Global
// ReSharper disable once CheckNamespace
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class Player : MonoBehaviour
{
    #region Fields
    
    // private:

    public int Wins { get; set; }
    public int PlayerNumber { get; set; }
    
    public bool Dead { get; set; }

    public Rigidbody center;

    #endregion

    private void OnEnable()
    {
        // Increase the number of current players
        GameManager.Instance.currentPlayers++;
        
        // Assign this player a number based on the amount of current players
        PlayerNumber = GameManager.Instance.currentPlayers;
        
        // Add new player to the list of players
        PlayerManager.Instance.players.Add(this);
        
        //Debug.Log(name + " " + PlayerNumber);
    }

    private void OnDisable()
    {
        // Increase the number of current players
        GameManager.Instance.currentPlayers--;
        
        // Remove player from the list of players
        PlayerManager.Instance.RemovePlayer(this);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        print("hit" + collision.gameObject.name);
        
        if (collision.gameObject.layer == LayerMask.NameToLayer($"Goal"))
        {
            // Increase the amount of wins this player has
            Wins++;
        
            GameManager.Instance.MatchWinner(this);
            GameManager.RoundWon();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer($"Death"))
        {
            print("death");
            
            if (GameManager.roundWon) return;
            
            // Set this player to be 'dead'
            Dead = true;

            // Remove the dead player from the list of active players
            PlayerManager.Instance.activePlayers.Remove(this);
            
            // Turn the gameObject off
            gameObject.SetActive(false);
            
            // Check if round has been won
            if (PlayerManager.Instance.activePlayers.Count == 1)
            {
                // Tell the game that the last remaining player is the winner
                GameManager.Instance.MatchWinner(PlayerManager.Instance.activePlayers[0]);
                GameManager.roundWon = true;
                
            }
        }
        
    }*/
}
