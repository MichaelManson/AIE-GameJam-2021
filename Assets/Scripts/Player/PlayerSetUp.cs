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

    public void SetLayers(string layer)
    {
        playerParts = new List<Transform>();

        // Get all children under player and assign their layer
        playerParts = GetComponentsInChildren<Transform>().ToList();

        for (int i = 0; i < playerParts.Count; i++)
        {
            playerParts[i].gameObject.layer = LayerMask.NameToLayer(layer);
        }
    }
}
