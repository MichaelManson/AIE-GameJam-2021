using UnityEngine;

public class MeleeCollision : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 2000.0f;

    private PlayerController playerController;

    public bool HasHit { get; set; } // Ensures the melee only hits once per tap

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        Debug.Assert(playerController != null, "playerController is null!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!HasHit)
        {
            // Attempt to get the rigidbody from the object of collision
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Debug.Log("Applying FORCE");

                rb.AddForce(transform.forward * knockbackForce);
                HasHit = true;
            }
            ////// Otherwise access the playerController of the enemy and apply force
            //else if (other.transform.parent.TryGetComponent<PlayerController>(out PlayerController enemy))
            //{
            //    if (enemy == null)
            //        return;

            //    Debug.Log("Knockback");

            //    enemy.Hips.AddForce(transform.forward * knockbackForce);
            //}

            if (other.transform.root.TryGetComponent<Health>(out Health health))
            {
                Debug.Log("Remaining health: " + health);
                health.TakeDamage(playerController.damage);

                HasHit = true;
            }
        }

    }

}
