using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels = new List<Level>();
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var level in levels)
        {
            print(level.sceneName);
        }
    }

    public void SetupLevels()
    {
        for (var index = 0; index < levels.Count; index++)
        {
            var level = levels[index];
            UIManager.Instance.levelImages[index].sprite = level.previewImage;
        }
    }
    
    public void LoadLevel(Level l)
    {
        SceneManager.LoadScene(l.sceneName);
    }
}
