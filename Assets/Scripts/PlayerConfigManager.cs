﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfigManager : MonoBehaviour
{
    public static PlayerConfigManager Instance;

    [SerializeField] private int lobbyWaitTime = 3;

    [SerializeField] private GameObject playerProfilePrefab;

    [SerializeField] private GameObject playerInfoPrefab;

    [SerializeField] private Canvas canvas;

    [SerializeField] private Timer timer;

    public Transform[] points;

    private List<PlayerConfig> playersJoined = new List<PlayerConfig>();
    private bool[] playersReady;

    private int count = 0;

    private bool allPlayersReady;

    private string[] layers = new string[4] { "Player1", "Player2", "Player3", "Player4" };

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
        input.transform.parent = null;
        input.transform.position = points[count].position;

        // Set up the player's layer
        input.transform.GetComponent<PlayerSetUp>().SetLayers(layers[count]);

        count++;

        PlayerConfig player = input.GetComponent<PlayerConfig>();
        player.PlayerId = count;
        SetUpPlayerInfo(player);

        Debug.Log("Player id: " + player.PlayerId);

        // Keep track of currently-joined players
        playersJoined.Add(player);

        Debug.Log(playersJoined.Count); // TO - DO: keep this
        // Hold another list of booleans 
    }

    public void UpdatePlayerProfiles(PlayerConfig player, bool isReady)
    {
        playersReady[player.PlayerId] = isReady;

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
            timer.StopAllCoroutines();
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
                timer.StopAllCoroutines();
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
        timer.BeginTimer(lobbyWaitTime);

        yield return new WaitForSeconds(3.0f);

        Debug.Log("All players ready. Proceed!");

        Reset();

        GameManager.Instance.Play();
    }

    private void Reset()
    {
        for(int i = 0; i < playersJoined.Count; i++)
        {
            // Set ready back to false
            playersJoined[i].IsReady = false;
            playersJoined[i].PlayerInfo.PlayerConfig_OnReadyEvent();
        }
    }

    public void SetUpPlayerInfo(PlayerConfig player)
    {
        PlayerInfo playerInfo = player.transform.GetComponentInChildren<PlayerInfo>();

        playerInfo.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        playerInfo.Initialise(player);
        
    }
}
