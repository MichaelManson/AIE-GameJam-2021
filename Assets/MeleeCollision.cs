using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hitting " + other.gameObject.name);

        other.GetComponent<Rigidbody>().AddForce(1000 * Vector3.right);
    }
}
