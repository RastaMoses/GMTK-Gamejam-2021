using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : MonoBehaviour
{
    [SerializeField] float moveSpeedToDock = 3f;
    //State

    public bool saved =  false;
    Vector3 dockPos;
    bool reachedDock = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DockingBay>() && !saved)
        {
            saved = true;
            Debug.Log("Collide with Dock");
            GetComponent<Sticky>().enabled = false;
            foreach (Transform child in transform)
            {
                child.GetComponent<Sticky>().Unstick();
            }

            transform.parent = null;
            gameObject.layer = 1;
            
            FindObjectOfType<Game>().ScoreIncrease();
            dockPos = other.GetComponent<DockingBay>().GetDockPosition();
            StartCoroutine(Save());

        }
    }

    IEnumerator Save()
    {
        
        while (!reachedDock)
        {
            transform.position = Vector3.MoveTowards(transform.position, dockPos, moveSpeedToDock * Time.fixedDeltaTime);
            if (transform.position == dockPos)
            {
                reachedDock = true;
            }

            transform.parent = null;
            yield return new WaitForFixedUpdate();
            GetComponent<SFX>().SaveSFX();
        }
        if (reachedDock)
        {
            
            Destroy(gameObject);
        }
        
    }
}
