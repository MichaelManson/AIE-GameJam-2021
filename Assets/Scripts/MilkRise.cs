using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkRise : MonoBehaviour
{
    public bool milkRises;
    float yPos = -15f;
    public float yPosSpeed = 1f;


    // Update is called once per frame
    void Update()
    {
        if (milkRises)
        {
            yPos += yPosSpeed * Time.deltaTime;
            this.transform.position = new Vector3(0, yPos, 0);
        }
    }
}
