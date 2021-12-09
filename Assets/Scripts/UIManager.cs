using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    
    public static UIManager Instance;
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(Instance);
        }

        Instance = this;
    }
    
    #endregion

    #region Fields
    
    // public:
    
    [Header("Canvases")] 
    public Canvas pauseCanvas;
    public Canvas winCanvas;
    
    public TextMeshProUGUI[] playerScores = new TextMeshProUGUI[4];
    
    public List<Image> levelImages = new List<Image>();
    
    [Space]
    
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winText;
    
    #endregion

    private void OnEnable()
    {
        GameManager.OnGameWon += GameWon;
        GameManager.OnMatchOver += MatchOver;
    }

    private void OnDisable()
    {
        GameManager.OnGameWon -= GameWon;
    }

    // Start is called before the first frame update
    private void Start()
    {
        //TurnPlayerScoresOn(false);
        
        ShowWinCanvas(false);

        timerText.text = "";
    }

    private void TurnPlayerScoresOn(bool turnOn = true)
    {
        foreach (var scoreText in playerScores)
        {
            scoreText.gameObject.SetActive(turnOn);
        }
    }

    public void ResetPlayerScores()
    {
        foreach (var scoreText in playerScores)
        {
            scoreText.text = "o";
        }
    }
    
    public void ShowPauseMenu(bool showPauseMenu = true) => pauseCanvas.gameObject.SetActive(showPauseMenu);
    private void ShowWinCanvas(bool showWinCanvas = true) => winCanvas.gameObject.SetActive(showWinCanvas);

    private void GameWon()
    {
        Debug.Log("Wow, delegate are awesome! " + name);
        ShowWinCanvas();
    }

    private void MatchOver()
    {
        
    }
}
