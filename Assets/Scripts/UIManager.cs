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

    public GameObject pauseScreen;

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
        
        HUDCanvas.gameObject.SetActive(false);

        //fadePanel.color = FadePanelColourClear;
    }

    private void TurnPlayerScoresOn(bool turnOn = true)
    {
        foreach (var scoreText in playerScores)
        {
            scoreText.gameObject.SetActive(turnOn);
        }
    }

    public void UpdatePlayerScores(bool turnOn = true)
    {
        for (var i = 0; i < PlayerManager.Instance.players.Count; i++)
        {
            var scoreText = playerScores[i];
            var wins = PlayerManager.Instance.players[i].Wins;

            scoreText.text = "";
            scoreText.text += wins > 9 ? "" + wins : "0" + wins;

            Debug.Log(scoreText.color);
        }
    }
    
    public void ResetPlayerScores()
    {
        foreach (var scoreText in playerScores)
        {
            scoreText.text = "00";
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

    public void Pause()
    {
        //if(pauseScreen.activeInHierarchy)
        //{
        //    pauseScreen.SetActive(false);
        //    GameManager.ResetForcesOnPlayers();
        //    Timer.PauseTimer();
        //    //Time.timeScale = 1.0f;
        //}
        //else
        //{
        //    pauseScreen.SetActive(true);
        //    GameManager.ResetForcesOnPlayers();
        //    //Time.timeScale = 0.0f;
        //}    
    }
}
