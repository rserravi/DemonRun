
using UnityEngine;

public class LavaJumper : MonoBehaviour
{
    public float explosionForce;
    public float explosionRadius;
    public float upward;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("IMPULSING");
        other.attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius,upward, ForceMode.Force);
    }
}
