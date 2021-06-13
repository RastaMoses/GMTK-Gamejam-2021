using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{

    public void Boost(Rigidbody rb, float maxSpeed, float boostForce)
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            return;
        }
        
        rb.AddRelativeForce(new Vector3(-transform.forward.y, transform.forward.x , 0f)* boostForce * Time.deltaTime);
    }
}
