using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBlocks : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider _collider;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
    }
     
    private void OnCollisionEnter(Collision collision)
    {
            // If the round hasn't started...
            if (GameManager.Instance.RoundStarted)
            {
                StartCoroutine(DropTile());
        }
    }

    IEnumerator DropTile()
    {
        yield return new WaitForSeconds(1f);
        rb.isKinematic = false;
        rb.useGravity = true;
    }

}
