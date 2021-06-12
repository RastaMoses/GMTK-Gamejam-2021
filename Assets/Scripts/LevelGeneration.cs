using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{   
    public GameObject block;
    [SerializeField] [Range(0,300)] int spawnChance = 64;
    public float elapsedTime = 0.0f;
    public float secondsBetweenSpawns = 1.5f;
    public bool spawn;

    private void Start()
    {
        StartCoroutine(Countdown());
    }
    private void SpawnBlocks()
    {
        int rand = Random.Range(0, spawnChance);
        if(rand == 0)
        {
            GameObject go = Instantiate(block, transform.position, Quaternion.identity);
            go.transform.SetParent(gameObject.transform);
        }
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
