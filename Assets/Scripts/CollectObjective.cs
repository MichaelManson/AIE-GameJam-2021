using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObjective : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        
        if (GameManager.Instance.CurrentLevelObjectiveType != LevelManager.LevelObjectiveType.CollectObject) return;

        GameManager.Instance.SetRoundWinner(PlayerManager.Instance.CheckIfCollidedWithPlayer(collision.gameObject.layer));
        
        
        
    }
}
