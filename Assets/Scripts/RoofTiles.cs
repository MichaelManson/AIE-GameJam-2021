using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofTiles : MonoBehaviour
{
    Rigidbody rb;
    public bool steppedOn;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }
}
