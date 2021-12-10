using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfigManager : MonoBehaviour
{
    public static PlayerConfigManager Instance;

    [SerializeField] private GameObject playerProfilePrefab;

    public Transform[] points;

    private List<PlayerConfig> playersJoined = new List<PlayerConfig>();
    private bool[] playersReady;

    private int count = 0;

    private bool allPlayersReady;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playersReady = new bool[4];
    }

    public void SpawnPlayerProfile(PlayerInput input)
    {
        //GameObject player = Instantiate(playerProfilePrefab);
        input.transform.parent = points[count];
        input.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        count++;

        PlayerConfig player = input.GetComponent<PlayerConfig>();
        player.playerId = count;

        Debug.Log("Player id: " + player.playerId);

        // Keep track of currently-joined players
        playersJoined.Add(player);

        Debug.Log(playersJoined.Count); // TO - DO: keep this
        // Hold another list of booleans 
    }

    public void UpdatePlayerProfiles(PlayerConfig player, bool isReady)
    {
        playersReady[player.playerId] = isReady;

        CheckPlayerProfiles();

        //if (isReady)
        //{
        //}
        //else
        //{

        //    // Decrease count
        //    count--;

        //    // Remove from player list
        //    playersJoined.Remove(player);

        //    Debug.Log(playersJoined.Count);
        //}
    }

    /// <summary>
    /// Checks if all players are readied up
    /// </summary>
    private void CheckPlayerProfiles()
    {
        if (playersJoined.Count == 0)
        {
            StopAllCoroutines();
            allPlayersReady = false;
            return;
        }

        // Assume all players are ready
        allPlayersReady = true;

        // Check if they all are ready
        for (int i = 0; i < playersJoined.Count; i++)
        {
            // If a player isn't ready, exit
            if (!playersJoined[i].IsReady)
            {
                Debug.Log("Not everyone is ready!");
                allPlayersReady = false;
                StopAllCoroutines();
            }
        }

        // If all players are ready, begin countdown to level
        if(allPlayersReady)
            StartCoroutine(BeginCountDown());
    }

    /// <summary>
    /// Temp function, will eventually use timer class
    /// </summary>
    /// <returns></returns>
    private IEnumerator BeginCountDown()
    {
        yield return new WaitForSeconds(3.0f);

        Debug.Log("All players ready. Proceed!");
    }
}
