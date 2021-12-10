using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Automatically selects the a selectable component
/// </summary>
public class Select : MonoBehaviour
{
    void Start()
    {
        GetComponent<Selectable>().Select();
    }
    
}
