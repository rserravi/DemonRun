using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaJumper : MonoBehaviour
{
    public float explosionForce;
    public float explosionRadius;
    public float upward;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("IMPULSING");
        other.attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius,upward, ForceMode.Force);
    }
}
