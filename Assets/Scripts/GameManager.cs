using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Singleton
    
    // ReSharper disable once MemberCanBePrivate.Global
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
        GameWon
    }
    
    #region Fields

    // public:

    [HideInInspector] public int currentPlayers = 0;

    public GameStates currentGameState;

    // private:
    
    private Timer _timer;
    private UIManager _ui;
    private LevelManager _level;

    private List<Level> _availableLevels = new List<Level>();

    private Player _lastWinner;
    
    #endregion

    #region Delegates
    
    public delegate void MatchWonHandler();
    public static MatchWonHandler OnRoundWon;
    
    public delegate void MatchOverHandler();
    public static MatchOverHandler OnRoundOver;
    
    public delegate void GameWonHandler();
    public static event GameWonHandler OnGameWon;

    public static void GameWon()
    {
        OnGameWon?.Invoke();
    }

    public static void MatchOver()
    {
        OnRoundOver?.Invoke();
    }

    public static void MatchWon()
    {
        OnRoundWon?.Invoke();
    }

    #endregion
    
    // Start is called before the first frame update
    private void Start()
    {
        _timer = GetComponent<Timer>();
        _level = GetComponent<LevelManager>();
        _ui = UIManager.Instance;

        _availableLevels = _level.levels;

        currentGameState = GameStates.Menu;
    }

    public void Play()
    {
        _ui.ResetPlayerScores();

        currentGameState = GameStates.Playing;
        
        StartMatch();
    }

    private void StartMatch()
    {
        GetRandomLevel();
        
        StopAllCoroutines();

        StartCoroutine(CountDown());
    }
    
    private void GetRandomLevel()
    {
        // If there aren't any available levels, add all levels back to the list
        if (_availableLevels.Count == 0) _availableLevels = _level.levels;
        
        // Pick a level at random from the list of available levels
        var r = Random.Range(0, _availableLevels.Count - 1);
        
        // Load chosen level
        _level.LoadLevel(_level.levels[r]);

        // Remove the chosen level from the available levels,
        // so the players don't play the same levels over and over again
        _availableLevels.Remove(_availableLevels[r]);
    }

    public void MatchWinner(Player player) => _lastWinner = player;

    public Player CheckGameWon() => PlayerManager.Instance.players.FirstOrDefault(p => p.Wins >= 12);

    private IEnumerator MatchIsOver()
    {
        Time.timeScale = 0.5f;
        
        yield return new WaitForSecondsRealtime(3);

        if (CheckGameWon() != null)
        {
            GameWon();
            yield break;
        }
        
        

    }
    
    private static IEnumerator CountDown(int duration = 3)
    {
        yield break;
    }
    
    public static void PauseGame()
    {
        Time.timeScale = 0f;

        UIManager.Instance.ShowPauseMenu(true);
    }
}
