using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{   
    public GameObject[] objects;
    public float elapsedTime = 0.0f;
    public float secondsBetweenSpawns = 1.5f;
    public bool spawn;

    private void Start()
    {
        StartCoroutine(Countdown());
    }
    private void SpawnBlocks()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject go = Instantiate(objects[rand], transform.position, Quaternion.identity);
        go.transform.SetParent(gameObject.transform);
    }

    private IEnumerator Countdown()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(secondsBetweenSpawns);
            SpawnBlocks();
            Debug.Log("spawned block"+gameObject.name);
        }
        

    }
}
