using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Fields
    
    // public:
    
    [Tooltip("Where a level will be spawned. The spawned level will become a child of this object.")]
    public Transform levelParent;
    
    public List<Level> levels = new List<Level>();

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
}
