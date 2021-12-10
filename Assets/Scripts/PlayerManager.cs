using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    
    // ReSharper disable once MemberCanBePrivate.Global
    public static PlayerManager Instance;
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(Instance);
        }

        Instance = this;
        
        DontDestroyOnLoad(this);
    }
    
    #endregion
    
    public List<Player> players = new List<Player>();
    public List<Player> activePlayers = new List<Player>();

    public static bool CanMove = true;
    public static bool CanJump = true;

    public List<Player> GetPlayersOrderedByScore()
    {
        var tempList = players;
        var temp = 0;
        
        // Bubble sort in descending order
        for (var j = 0; j <= tempList.Count - 2; j++) 
        {
            for (var i = 0; i <= tempList.Count - 2; i++)
            {
                // ReSharper disable once InvertIf
                if (tempList[i].PlayerNumber < tempList[i + 1].PlayerNumber)
                {
                    temp = tempList[i + 1].PlayerNumber;
                    tempList[i + 1].PlayerNumber = tempList[i].PlayerNumber;
                    tempList[i].PlayerNumber = temp;
                }
            }
        }

        return tempList;
    }

    public void RemovePlayer(Player player)
    {
        // Remove the player from the list of active players
        players.Remove(player);

        // Adjust the player number for each active player
        foreach (var p in players.Where(p => p.PlayerNumber > player.PlayerNumber))
        {
            p.PlayerNumber--;
        }
    }

    public Player CheckIfCollidedWithPlayer(int layer)
    {
        switch (layer)
        {
            case 8: // Player1
                return players[0];
                break;
            case 9: // Player2
                return players[1];
                break;
            case 10: // Player3
                return players[2];
                break;
            case 11: // Player4
                return players[3];
                break;
            default:
                return null;
        }
    }
}
