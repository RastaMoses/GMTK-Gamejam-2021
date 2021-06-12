using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    public bool sticksToPlayer;

    string originalTag;
    private void Start()
    {
        originalTag = gameObject.tag;
    }
    private void OnCollisionEnter(Collision collision)
    {
        var col = collision;
        Debug.Log("Collision");
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
            this.gameObject.transform.SetParent(col.collider.gameObject.transform, true);
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            //Add mass to player
            FindObjectOfType<Player>().gameObject.GetComponent<Rigidbody>().mass += this.gameObject.GetComponent<Rigidbody>().mass;
        }
    }

    
    
    
}
