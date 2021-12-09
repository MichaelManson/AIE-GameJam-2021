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
}
