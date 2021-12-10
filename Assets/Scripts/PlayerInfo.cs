using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerText;
    [SerializeField] private TextMeshProUGUI readyText;

    private PlayerConfig trackedPlayer;

    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        readyText.enabled = false;

        mainCam = Camera.main;
    }

    public void Initialise(PlayerConfig player)
    {
        playerText.text = "P" + player.PlayerId;
        trackedPlayer = player;
    }

    private void Update()
    {
        if (!mainCam) mainCam = Camera.main;
        
        // Ensures the ui is always looking directly at the camera
        transform.forward = mainCam.transform.forward;
    }

    public void PlayerConfig_OnReadyEvent()
    {
        Debug.Log("Calling ready ui");
        readyText.enabled = !readyText.enabled;
    }
}
