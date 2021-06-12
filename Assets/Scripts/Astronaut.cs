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
        if (other.GetComponent<DockingBay>())
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Sticky>().Unstick();
            }
            transform.parent = null;
            gameObject.layer = 1;
            saved = true;
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
            yield return new WaitForFixedUpdate();
        }
        if (reachedDock)
        {
            FindObjectOfType<Game>().ScoreIncrease();
            Destroy(gameObject);
        }
        
    }
}
