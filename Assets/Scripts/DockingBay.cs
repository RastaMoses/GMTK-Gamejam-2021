using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingBay : MonoBehaviour
{
    [SerializeField] Transform dock;



    public Vector3 GetDockPosition()
    {
        return dock.position;
    }
}
