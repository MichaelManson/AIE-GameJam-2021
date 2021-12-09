using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public static HealthBarManager Instance;

    [SerializeField] private GameObject healthPrefab;

    private void Awake()
    {
        Instance = this;
    }
    
    public HealthBar AddHealthBar(Health health)
    {
        GameObject healthBar = Instantiate(healthPrefab);
        healthBar.transform.parent = transform;
        HealthBar hb = healthBar.GetComponent<HealthBar>();
        hb.health = health;
        return hb;
    }
}
