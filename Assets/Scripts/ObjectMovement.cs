using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] float speed = 1f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
       
    }

    private void FixedUpdate()
    {
        Vector2 Movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.AddForce(Movement);
    }
}
