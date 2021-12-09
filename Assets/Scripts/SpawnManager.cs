using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private Transform[] spawnPositions = new Transform[4];

    /*public void SpawnCharacters(Character[] characters)
    {
        for (var i = 0; i < characters; i++)
        {
            characters[i].Spawn[spawnPositions[i]];
        }
    }*/
    
    
    // Start is called before the first frame update
    void Start()
    {
        print("hi");
    }
}
