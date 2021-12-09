using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public GameObject levelGeo;
    
    public Sprite previewImage;
    
    public LevelManager.LevelObjectiveType levelObjectiveType;
    
    [TextArea(minLines:3, maxLines:6)]
    public string objectiveDescription;
}
