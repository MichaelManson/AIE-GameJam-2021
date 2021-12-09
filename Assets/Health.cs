using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;

    HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate a new health bar and place it over the player
        healthBar = HealthBarManager.Instance.AddHealthBar(this);
    }

    public void TakeDamage(float damage)
    {
        // Subtract the damage
        health -= damage;

        // Update our health bar
        if (healthBar)
            healthBar.UpdateMeter();
    }

}
