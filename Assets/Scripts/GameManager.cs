﻿using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
// ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable once MemberCanBePrivate.Global

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

    public enum GameStates
    {
        Menu = 0, 
        Playing,
        GameOver
    }
    
    #region Fields

    // public:

    [HideInInspector] public int currentPlayers = 0;

    public GameStates currentGameState;

    public Player RoundWinner => _lastWinner;

    // private:

    [SerializeField] private int scoreToWin;
    
    private Timer _timer;
    private UIManager _ui;
    private LevelManager _level;

    private List<Level> _availableLevels = new List<Level>();

    private Player _lastWinner;
    
    #endregion

    #region Delegates
    
    public delegate void MatchWonHandler();
    public static event MatchWonHandler OnRoundWon;
    
    public delegate void MatchOverHandler();
    public static event MatchOverHandler OnRoundOver;
    
    public delegate void GameWonHandler();
    public static event GameWonHandler OnGameWon;

    private void OnEnable()
    {
        OnRoundWon += RoundIsOver;
        OnRoundOver += RoundIsOver;
    }

    private void OnDisable()
    {
        OnRoundWon -= RoundIsOver;
        OnRoundOver -= RoundIsOver;
    }

    public static void GameWon() => OnGameWon?.Invoke();

    public static void RoundOver() => OnRoundOver?.Invoke();

    public static void RoundWon() => OnRoundWon?.Invoke();

    #endregion
    
    private void Start()
    {
        _timer = GetComponent<Timer>();
        _level = GetComponent<LevelManager>();
        _ui = UIManager.Instance;

        _availableLevels = new List<Level>(_level.levels);

        //_lastWinner = PlayerManager.Instance.players[0];
        
        currentGameState = GameStates.Menu;
    }

    public void Play()
    {
        //_ui.ResetPlayerScores();

        currentGameState = GameStates.Playing;
        
        NewRound();
    }

    public void SpawnCharacters()
    {
        // Find all the spawn locations of the current level
        var spawnLocations = GameObject.Find("Spawns").GetComponentsInChildren<Transform>();
        
        // Spawn every player at the appropriate position
        foreach (var player in PlayerManager.Instance.players)
        {
            player.center.transform.position = spawnLocations[player.PlayerNumber].transform.position;
        }
    }
    
    private void LoadRandomLevel()
    {
        // If there aren't any available levels, add all levels back to the list
        if (_availableLevels.Count == 0) _availableLevels = _level.levels;

        // Pick a level at random from the list of available levels
        var r = Random.Range(0, _availableLevels.Count - 1);

        // Store references to the random level
        var level = _availableLevels[r];
        _level.currentLevel = level;
        
        // Load chosen level
        _level.LoadLevel(level);

        // Remove the chosen level from the available levels,
        // so the players don't play the same levels over and over again
        _availableLevels.Remove(level);
    }

    public void MatchWinner(Player player) => _lastWinner = player;

    private bool CheckGameWon()
    {
        // Loop through every player in the game
        foreach (var p in PlayerManager.Instance.players)
        {
            // If one of them has the win score
            if (p.Wins == scoreToWin)
            {
                // Then the game is won
                return true;
            }
        }

        return false;
    }

    #region Async Tasks

    private async void RoundIsOver()
    {
        // Slow down time at the end like Stick Fight
        await SlowTime();

        // Prepare new round
        await NewRound();
    }

    private async Task NewRound()
    {
        // Set time to 1x (normal) speed
        Time.timeScale = 1f;
        
        // Do the level transition animation
        await DoLevelTransitionAnimation();
        
        // Wait half a second
        await Task.Delay(500);
        
        
        // At this point we are right in the middle of the transition
        // Now is the time to load levels and do any other necessary checks
        

        // Turn off win text
        UIManager.Instance.winCanvas.gameObject.SetActive(false);
        
        print("NOW");
        
        // Load a random level
        LoadRandomLevel();
        
        // Spawn all the characters in the right spot
        SpawnCharacters();

        // Check if a player has won the game
        if (CheckGameWon())
            GameWon();

        await Task.Delay(2000);
        
        await Countdown();
        
        _timer.BeginTimer(5);
    }
    
    internal static async Task SlowTime()
    {
        // Slow time to 0.5x speed
        Time.timeScale = 0.5f;
        
        // Wait 3 seconds (realtime)
        await Task.Delay(3000);
    }
    
    internal async Task DoLevelTransitionAnimation()
    {
        _ui.fadePanels[0].GetComponent<Animation>().Play();
        _ui.fadePanels[1].GetComponent<Animation>().Play();
        
        await Task.Yield();
    }

    internal async Task Countdown()
    {
        var countdownAnimation = _ui.countdownText.GetComponent<Animation>();
        
        _ui.countdownText.text = "3";
        countdownAnimation.Play();

        await Task.Delay(1000);
        
        countdownAnimation.Stop();
        _ui.countdownText.text = "2";
        countdownAnimation.Play();

        await Task.Delay(1000);
        
        countdownAnimation.Stop();
        _ui.countdownText.text = "1";
        countdownAnimation.Play();

        await Task.Delay(1000);

        countdownAnimation.Stop();
        _ui.countdownText.text = "GO!";
        countdownAnimation.Play();

        await Task.Delay(1000);

        countdownAnimation.Stop();
        _ui.countdownText.text = "";
    }

    #endregion
    
    public static void PauseGame()
    {
        Time.timeScale = 0f;

        UIManager.Instance.ShowPauseMenu(true);
    }
}
