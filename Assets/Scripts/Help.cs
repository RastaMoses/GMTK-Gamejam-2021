using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    public GameObject help;

    public void Start()
    {
        help.SetActive(false);
    }
    public void OnMouseOver()
    {
        help.SetActive(true);
    }
    public void OnMouseExit()
    {
        help.SetActive(false);
    }
}
