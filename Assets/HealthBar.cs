using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image primaryFill;

    public Health health;

    private void Start()
    {
        if (primaryFill)
        {
            primaryFill.type = Image.Type.Filled;
            primaryFill.fillMethod = Image.FillMethod.Horizontal;
        }
    }

    private void Update()
    {
        if (health != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(health.transform.position);

            transform.position = pos;/*health.transform.position + Vector3.up * 2;*/
            //transform.forward = Camera.main.transform.forward;
            // Needs 
            //transform.forward = FindObjectOfType<Camera>().GetComponent<CameraController>().Target.forward;
            UpdateMeter();
        }
    }

    public void UpdateMeter()
    {
        // Scale the meter
        float pct = Mathf.Clamp01(health.health / health.maxHealth);
        primaryFill.fillAmount = pct;
        
    }
}
