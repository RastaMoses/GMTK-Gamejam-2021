using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    //Serialize Params
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionForce;

    private void OnCollisionEnter(Collision collision)
    {
        if (GetComponent<Renderer>().isVisible)
        {
            Explosion();
        }
    }

    void Explosion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            
            if (hitCollider.GetComponent<Sticky>())
            {
                hitCollider.GetComponent<Sticky>().Unstick();
            }
            if (hitCollider.GetComponent<Rigidbody>())
            {
                hitCollider.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, 0);
            }
            
        }
        Destroy(gameObject);
    }
}
