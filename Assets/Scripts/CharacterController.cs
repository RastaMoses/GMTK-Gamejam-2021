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


    //State
    bool onCooldown = false;
    Vector3 mouseWorldPosition;
    bool tooHeavy = false;

    //cached comp
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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


        float angle = AngleBetweenPoints(transform.position, mouseWorldPosition);

        
        transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(new Vector3(0f, 0f, angle)),rotateSpeed);
        

        

        
        
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
        if (Input.GetKey(KeyCode.Space))
        {
            Boost();
        }
    }

    void Boost()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            return;
        }
        rb.AddRelativeForce(Vector3.left * boostForce * Time.deltaTime);
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
        StartCoroutine(ResetEject());
    }

    IEnumerator ResetEject()
    {
        onCooldown = true;
        yield return new WaitForSeconds(ejectCooldown);
        onCooldown = false;
    }





}

