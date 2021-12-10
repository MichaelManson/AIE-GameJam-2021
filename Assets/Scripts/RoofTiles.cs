using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofTiles : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider _collider;
    public bool steppedOn = false;

    private string[] layers = { "Player1", "Player2", "Player3", "Player4" };

    LayerMask layerMask;

    private void OnEnable()
    {
        // Set up layerMask
        layerMask = LayerMask.GetMask(layers);     
        
        rb = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
    }

    //just for testing
    private void Update()
    {
        Debug.Log("Timer status: " + GameManager.Instance.RoundStarted);

        if (steppedOn == true)
        {
            StartCoroutine(DropTile());
        }      
    }
     
    private void OnCollisionEnter(Collision collision)
    {
        // Check if player hit the roof...
        if ((layerMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            // If the round hasn't started...
            if (GameManager.Instance.RoundStarted)
            {
                StartCoroutine(DropTile());
            }
            // Delay the drop
            else
            {
                StartCoroutine(DropTileAtStart());
            }
        }
    }

    IEnumerator DropTile()
    {
        yield return new WaitForSeconds(1f);
        rb.isKinematic = false;
        rb.useGravity = true;
        _collider.enabled = false;
    }

    private IEnumerator DropTileAtStart()
    {
        yield return new WaitForSeconds(6f);
        rb.isKinematic = false;
        rb.useGravity = true;
        _collider.enabled = false;
    }
}
