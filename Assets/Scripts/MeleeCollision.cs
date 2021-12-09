using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.TryGetComponent<PlayerController>(out PlayerController something))
        {
            if (something == null)
                return;
            Debug.Log("Knockback");

            something.Hips.AddForce(Vector3.right * 3000.0f);
        }

    }
}
