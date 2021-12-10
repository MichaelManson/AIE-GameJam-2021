using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObjective : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        
        if (GameManager.Instance.CurrentLevelObjectiveType != LevelManager.LevelObjectiveType.CollectObject) return;

        GameManager.Instance.SetRoundWinner(PlayerManager.Instance.CheckIfCollidedWithPlayer(other.gameObject.layer));

        GameManager.Instance.RoundWinner.Wins++;
        
        GameManager.Instance.RoundIsOver();
        
        GameManager.roundWon = true;
    }
}
