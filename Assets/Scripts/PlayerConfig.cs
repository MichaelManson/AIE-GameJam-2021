using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerConfig : MonoBehaviour
{
    public Image playerProfile;

    public TextMeshProUGUI playerText;
    public TextMeshProUGUI readyText;

    public int playerId; // Used to identify player number (1, 2,3,4)

    public bool IsReady {get; set;}

    private void Start()
    {
        // Set up player index name (Player 1, Player 2 etc)
        playerText.text = "Player " + playerId;
    }

    public void Ready_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Player interacting is:" + playerId);
        IsReady = !IsReady;

        UpdateUI();

        // Sends a message to the manager to update ready players
        PlayerConfigManager.Instance.UpdatePlayerProfiles(this, IsReady);
    }

    private void UpdateUI()
    {
        if(IsReady)
        {
            // Display ready text
            readyText.enabled = true;
        }
        else
        {
            readyText.enabled = false;
        }
    }
}
