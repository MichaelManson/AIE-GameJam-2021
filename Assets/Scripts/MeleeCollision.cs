using UnityEngine;

public class MeleeCollision : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 2000.0f;

    private void OnTriggerEnter(Collider other)
    {
        // Attempt to get the rigidbody from the object of collision
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if(rb != null)
        {
            rb.AddForce(transform.forward * knockbackForce);
        }
        // Otherwise access the playerController of the enemy and apply force
        else if(other.transform.parent.TryGetComponent<PlayerController>(out PlayerController enemy))
        {
            if (enemy == null)
                return;

            Debug.Log("Knockback");

            enemy.Hips.AddForce(transform.forward * knockbackForce);
        }


       if(other.transform.parent.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(10.0f);
        }

    }
}
