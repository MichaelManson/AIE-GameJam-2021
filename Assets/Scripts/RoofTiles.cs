using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofTiles : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider _collider;
    public bool steppedOn = false;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
    }

    //just for testing
    private void Update()
    {
        if (steppedOn == true)
        {
            DropTile();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DropTile();
        }
    }

    IEnumerator DropTile()
    {
        yield return new WaitForSeconds(1f);
        rb.useGravity = true;
        _collider.enabled = false;
    }
}
