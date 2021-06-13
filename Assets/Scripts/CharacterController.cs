using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Serialize params
    [SerializeField] float rotateSpeed;
    [SerializeField] float boostForce = 5f;
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float maxMass = 100f;

    [SerializeField] float ejectForce = 200f;
    [SerializeField] float ejectRadius = 5f;
    [SerializeField] float ejectCooldown = 10f;

    [SerializeField] GameObject jetpack;




    //State
    bool onCooldown = false;
    Vector3 mouseWorldPosition;
    bool tooHeavy = false;
    float minMass;

    //cached comp
    public Rigidbody rb;

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        minMass = rb.mass;
    }
    void FixedUpdate()
    {
        if (rb.mass > maxMass)
        {
            tooHeavy = true;
        }
        else
        {
            tooHeavy = false;
        }
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 100f);
        if (rb.velocity.magnitude > 100)
        {
            rb.velocity = Vector3.zero;
        }
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 100f);


        float angle = AngleBetweenPoints(jetpack.transform.position, mouseWorldPosition);

        
        jetpack.transform.rotation = Quaternion.RotateTowards(jetpack.transform.rotation,Quaternion.Euler(new Vector3(angle, -90f, 0f)),rotateSpeed);

        

        //transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(new Vector3(0, 0, Mathf.Asin(horizontal))), rotateSpeed * Time.fixedDeltaTime);
        

        
        
    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!onCooldown)
            {
                Eject();
            }

        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            jetpack.GetComponent<Jetpack>().Boost(rb, maxSpeed, boostForce);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
        }

    }

    

    public bool GetHeavy()
    {
        return tooHeavy;
    }

    void Eject()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Sticky>())
            {
                child.GetComponent<Sticky>().Unstick();
            }
            
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ejectRadius);
        foreach (var hitCollider in hitColliders)
        {

            if (hitCollider.GetComponent<Sticky>())
            {
                hitCollider.GetComponent<Sticky>().Unstick();
            }
            if (hitCollider.GetComponent<Rigidbody>() && hitCollider.GetComponent<CharacterController>() == false)
            {
                hitCollider.GetComponent<Rigidbody>().AddExplosionForce(ejectForce, transform.position, 0);
            }

        }
        GetComponent<SFX>().EjectSFX();
        StartCoroutine(ResetEject());
    }

    IEnumerator ResetEject()
    {
        onCooldown = true;
        yield return new WaitForSeconds(ejectCooldown);
        onCooldown = false;
    }


    public GameObject GetRB()
    {
        return rb.gameObject;
    }

}

