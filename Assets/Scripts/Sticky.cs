using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    //[SerializeField] float explosionForce = 5f;
    //[SerializeField] float explosionRadius = 1f;
    public bool sticksToPlayer;

    string originalTag;
    private void Start()
    {
        originalTag = gameObject.tag;
    }
    private void OnCollisionEnter(Collision collision)
    {
        var col = collision;
        
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Collision with Player");
            gameObject.layer = LayerMask.NameToLayer("Player");
            this.gameObject.transform.SetParent(col.collider.gameObject.transform, true);
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            //Add mass to player
            FindObjectOfType<Player>().gameObject.GetComponent<Rigidbody>().mass += this.gameObject.GetComponent<Rigidbody>().mass;
            //Remove velocity
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    
    public void Unstick()
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        FindObjectOfType<Player>().gameObject.GetComponent<Rigidbody>().mass -= this.gameObject.GetComponent<Rigidbody>().mass;
        gameObject.layer = LayerMask.NameToLayer("Blocks");
        gameObject.transform.parent = null;
        foreach (Transform child in transform)
        {
            child.GetComponent<Sticky>().Unstick();
        }
        //GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
    }
    
}
