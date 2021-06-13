using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    //[SerializeField] float explosionForce = 5f;
    //[SerializeField] float explosionRadius = 1f;
    public bool sticksToPlayer = true;
    [SerializeField] float stickyResetTime = 1f;

    string originalTag;
    private void Start()
    {
        originalTag = gameObject.tag;
    }
    private void OnCollisionEnter(Collision collision)
    {
        var col = collision;
        if (!sticksToPlayer)
        {
            return;
        }

        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (FindObjectOfType<CharacterController>().GetHeavy())
            {
                Unstick();
                return;
            }
            gameObject.layer = LayerMask.NameToLayer("Player");
            this.gameObject.transform.SetParent(col.collider.gameObject.transform, true);
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            //Add mass to player
            FindObjectOfType<Player>().gameObject.GetComponent<Rigidbody>().mass += this.gameObject.GetComponent<Rigidbody>().mass;
            //Remove velocity
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<Rigidbody>().freezeRotation = true;

            GetComponent<SFX>().StickSFX();
        }
    }

    
    public void Unstick()
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        FindObjectOfType<Player>().gameObject.GetComponent<Rigidbody>().mass -= this.gameObject.GetComponent<Rigidbody>().mass;
        gameObject.layer = LayerMask.NameToLayer("Blocks");
        gameObject.transform.parent = null;
        GetComponent<Rigidbody>().freezeRotation = false;
        StartCoroutine(ResetSticky());
        
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Sticky>())
            {
                child.GetComponent<Sticky>().Unstick();
            }
        }
        //GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
    }

    IEnumerator ResetSticky()
    {
        sticksToPlayer = false;
        yield return new WaitForSeconds(stickyResetTime);
        sticksToPlayer = true;
    }
    
}
