using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Serialize params
    [SerializeField] float rotateSpeed;


    //State
    Vector3 mouseWorldPosition;

    //cached comp
    Rigidbody rb;
    void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
        
        //Mouse Position in the world. It's important to give it some distance from the camera. 
        //If the screen point is calculated right from the exact position of the camera, then it will
        //just return the exact same position as the camera, which is no good.

        //Angle between mouse and this object
        float angle = AngleBetweenPoints(transform.position, mouseWorldPosition);

        //Ta daa
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        

    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
}

