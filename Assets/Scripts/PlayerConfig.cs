using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerConfig : MonoBehaviour
{
    public Image playerProfile;

    public int PlayerId { get; set; } // Used to identify player number (1, 2,3,4)

    public bool IsReady {get; set;}

    public GameObject playerPrefabTest;

    public PlayerInfo PlayerInfo { get; set; }

    private void Awake()
    {
        PlayerInfo = transform.GetComponentInChildren<PlayerInfo>();
        Debug.Assert(PlayerInfo != null, "playerInfo is null");
    }

    public void Ready_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Player interacting is:" + PlayerId);
        IsReady = !IsReady;

        // Sends a message to the manager to update ready players
        PlayerConfigManager.Instance.UpdatePlayerProfiles(this, IsReady);

        // Enabled / Disable the ready text
        PlayerInfo.PlayerConfig_OnReadyEvent();
    }
}
