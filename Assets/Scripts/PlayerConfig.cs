﻿using System.Collections;
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

    public bool IsReady {get; set;}

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();

        playerControls.Profile.Ready.performed += Ready_performed;   
    }

    private void OnDisable()
    {
        playerControls.Disable();

        playerControls.Profile.Ready.performed -= Ready_performed;
    }

    private void Ready_performed(InputAction.CallbackContext obj)
    {
        IsReady = !IsReady;

        Debug.Log("Ready state: " + IsReady);

        PlayerConfigManager.Instance.OnReady();
    }
}
