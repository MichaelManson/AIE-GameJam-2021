using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public enum LevelObjectiveType
    {
        Deathmatch,
        CollectObject,
            
    }
    
    #region Fields
    
    // public:
    
    [Tooltip("Where a level will be spawned. The spawned level will become a child of this object.")]
    public Transform levelParent;
    
    public List<Level> levels = new List<Level>();

    [SerializeField] private Level winLevel;
    
    [HideInInspector] public Level currentLevel;

    // protected:
    
    
    // private:
    
    private GameObject _currentLevelGeo;

    #endregion
    
    private void Start()
    {
        //SetupLevels();
    }

    private void SetupLevels()
    {
        if (UIManager.Instance.levelImages.Count == 0) return;
        
        // Loop through all the levels and update the level select buttons to have the right background
        for (var i = 0; i < levels.Count; i++)
        {
            var level = levels[i];
            UIManager.Instance.levelImages[i].sprite = level.previewImage;
        }
    }

    /// <summary>
    /// Loads a new Level into the scene.
    /// </summary>
    public void LoadLevel(Level l)
    {
        // If a level is already loaded, destroy the old one...
        if (_currentLevelGeo) Destroy(_currentLevelGeo);

        // and load the new one
        _currentLevelGeo = Instantiate(l.levelGeo, levelParent);
    }

    public void LoadWinLevel()
    {
        LoadLevel(winLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        Destroy(GameObject.Find("GameManager"));

        var allClones = GameObject.FindGameObjectsWithTag("Player");
        foreach (var playerClone in allClones)
        {
            Destroy(playerClone);
        }

        SceneManager.LoadScene("_Start", LoadSceneMode.Single);
    }
}
