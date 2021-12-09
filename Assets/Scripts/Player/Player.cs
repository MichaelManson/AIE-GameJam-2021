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

    public int Wins { get; private set; }
    public int PlayerNumber { get; set; }

    #endregion

    private void OnEnable()
    {
        // Increase the number of current players
        GameManager.Instance.currentPlayers++;
        
        // Assign this player a number based on the amount of current players
        PlayerNumber = GameManager.Instance.currentPlayers;
        
        // Add new player to the list of players
        PlayerManager.Instance.players.Add(this);
        
        Debug.Log(name + " " + PlayerNumber);
    }

    private void OnDisable()
    {
        // Increase the number of current players
        GameManager.Instance.currentPlayers--;
        
        // Remove player from the list of players
        PlayerManager.Instance.RemovePlayer(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ReSharper disable once Unity.UnknownLayer
        if (collision.gameObject.layer != LayerMask.NameToLayer("Goal")) return;

        // Increase the amount of wins this player has
        Wins++;
        
        GameManager.Instance.MatchWinner(this);
        GameManager.OnRoundWon();
    }
}
