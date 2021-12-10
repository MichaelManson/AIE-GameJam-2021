using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Used to set up the player's layers
/// and their displayed ui
/// </summary>
public class PlayerSetUp : MonoBehaviour
{
    private List<Transform> playerParts;

    [SerializeField] private Transform ragdollGroup;

    [SerializeField] private Transform hatPlacement;

    public void SetLayers(string layer)
    {
        playerParts = new List<Transform>();

        // Get all children under player and assign their layer
        playerParts = ragdollGroup.GetComponentsInChildren<Transform>(true).ToList();

        for (int i = 0; i < playerParts.Count; i++)
        {
            playerParts[i].gameObject.layer = LayerMask.NameToLayer(layer);
        }
    }

    public void SetHat(PlayerConfig player)
    {
        Debug.Log("PLAYER ID: " + player.PlayerId);

        // Spawn a hat
        GameObject hat = Instantiate(PlayerConfigManager.Instance.playerHats[player.PlayerId - 1]);

        // Position the hat on top of the player's head
        hat.transform.parent = hatPlacement;
        hat.transform.localPosition = Vector3.zero;
    }
}
