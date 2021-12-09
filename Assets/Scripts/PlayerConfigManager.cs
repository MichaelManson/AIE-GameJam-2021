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

    private int count = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnPlayerProfile(PlayerInput input)
    {
        //GameObject player = Instantiate(playerProfilePrefab);
        input.transform.parent = points[count];
        input.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        count++;

        // Keep track of currently-joined players
        playersJoined.Add(input.GetComponent<PlayerConfig>());
    }


    /// <summary>
    /// Sent by PlayerConfig, the manager
    /// checks if all players have readied up
    /// </summary>
    public void OnReady()
    {
        if (playersJoined.Count == 0)
        {
            StopAllCoroutines();
            return;
        }

        for(int i = 0; i < playersJoined.Count; i++)
        {
            // If a player isn't ready, exit
            if (!playersJoined[i].IsReady)
            {
                StopAllCoroutines();
                return;
            }
        }

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
