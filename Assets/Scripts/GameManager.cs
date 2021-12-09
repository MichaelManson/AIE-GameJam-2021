using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    
    public static GameManager Instance;
    
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
    
    private Timer _timer;
    private UIManager _ui;
    
    // Start is called before the first frame update
    private void Start()
    {
        _timer = GetComponent<Timer>();
        _ui = UIManager.Instance;
    }
    
    public static void PauseGame()
    {
        Time.timeScale = 0f;

        UIManager.Instance.ShowPauseMenu(true);
    }

    public void GameWon(/*Character c*/)
    {
        _ui.ShowWinCanvas();
    }
}
