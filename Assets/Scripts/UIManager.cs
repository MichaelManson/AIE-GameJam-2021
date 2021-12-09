using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [Header("Canvases")] 
    public Canvas pauseCanvas;
    public Canvas winCanvas;
    
    public TextMeshProUGUI[] playerScores = new TextMeshProUGUI[4];
    
    [Space]
    
    public TextMeshProUGUI timerText;
    
    // Start is called before the first frame update
    private void Start()
    {
        TurnPlayerScoresOn(false);
    }

    private void TurnPlayerScoresOn(bool turnOn = true)
    {
        foreach (var scoreText in playerScores)
        {
            scoreText.gameObject.SetActive(turnOn);
        }
    }

    public void ShowPauseMenu(bool showPauseMenu = true) => pauseCanvas.gameObject.SetActive(showPauseMenu);
    
    public void ShowWinCanvas(bool showWinCanvas = true) => winCanvas.gameObject.SetActive(showWinCanvas);

}
