using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Serialize params
    [SerializeField] float rotateSpeed;
    [SerializeField] float boostForce = 5f;


    //State
    Vector3 mouseWorldPosition;

    //cached comp
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
        
        
        float angle = AngleBetweenPoints(transform.position, mouseWorldPosition);

        
        transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(new Vector3(0f, 0f, angle)),rotateSpeed);
        

    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Boost();
        }
    }

    void Boost()
    {
        rb.AddRelativeForce(Vector3.left * boostForce);
    }






}

