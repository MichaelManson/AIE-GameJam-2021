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
    public Canvas HUDCanvas;
    
    public TextMeshProUGUI[] playerScores = new TextMeshProUGUI[4];
    
    public List<Image> levelImages = new List<Image>();
    
    [Space]
    
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI objectiveText;

    [Space] public Image[] fadePanels = new Image[2];
    
    #endregion

    private void OnEnable()
    {
        GameManager.OnGameWon += GameWon;
        GameManager.OnRoundWon += RoundWon;
        GameManager.OnRoundOver += RoundOver;
    }

    private void OnDisable()
    {
        GameManager.OnGameWon -= GameWon;
        GameManager.OnRoundWon -= RoundWon;
        GameManager.OnRoundOver -= RoundOver;
    }

    // Start is called before the first frame update
    private void Start()
    {
        //TurnPlayerScoresOn(false);
        
        winText.gameObject.SetActive(false);
        
        if (timerText) timerText.text = "";
        
        if (objectiveText) objectiveText.text = "";

        //fadePanel.color = FadePanelColourClear;
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

    private void GameWon()
    {
        Debug.Log("Wow, delegate are awesome! " + name);
        winText.gameObject.SetActive(true);
    }

    private void RoundOver()
    {
        timerText.gameObject.SetActive(false);

        winText.gameObject.SetActive(true);
        
        winText.text = "Round Over...";
    }

    private void RoundWon()
    {
        timerText.gameObject.SetActive(false);
        
        winText.gameObject.SetActive(true);

        winText.text = "Player " + GameManager.Instance.RoundWinner.PlayerNumber + " Wins The Round!";
    }
}
