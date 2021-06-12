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
        Debug.Log("Collision");
        if (collision.collider.CompareTag("Player"))
        {
            gameObject.tag = "Player";
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY;
            //gameObject.transform.SetParent(collision.collider.transform, true);
            gameObject.GetComponent<FixedJoint>().connectedBody = collision.gameObject.GetComponent<Rigidbody>();
            Debug.Log("Player Collision");
        }
    }

    
    private void OnJointBreak(float breakForce)
    {
        gameObject.tag = originalTag;
        gameObject.AddComponent<FixedJoint>();
    }
    
}
