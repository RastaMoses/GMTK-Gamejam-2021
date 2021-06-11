using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{   
    public GameObject[] objects;
    public float elapsedTime = 0.0f;
    private float secondsBetweenSpawns = 2f;  
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >secondsBetweenSpawns)
        {
            elapsedTime = 0;
            SpawnBlocks();
        } 
    }
    private void SpawnBlocks()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject go = Instantiate(objects[rand], transform.position, Quaternion.identity);
        go.transform.SetParent(gameObject.transform);
    }
}
