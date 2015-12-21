using UnityEngine;

public class CollisionReaction : MonoBehaviour
{
    [Range(1, 5)]
    public float restitutionForceMultiplier = 1;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        rb.AddForce(collision.impulse * (restitutionForceMultiplier - 1), ForceMode.Impulse);
    }
}
