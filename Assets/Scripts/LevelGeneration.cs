using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] float minSpawnDistance = 3f;
    [SerializeField] float maxSpawnDistance = 6f;
    [SerializeField] float randomPos = 0.5f;
    [SerializeField] float randomPosZ = 0.1f;
    [SerializeField] float randomRotation = 1f;
    [SerializeField] float randomTorque = 10f;
    [SerializeField] float randomMinSize = 0.5f;
    [SerializeField] float randomMaxSize = 1.6f;
    [SerializeField] float randomSpeedMin = 0f;
    [SerializeField] float randomSpeedMax = 5f;
    public GameObject[] blocks;
    GameObject block;
    [SerializeField] [Range(0,100)] int spawnChance = 64;
    //public float elapsedTime = 0.0f;
    public float secondsBetweenSpawns = 1.5f;
    public bool spawn;

    private void Start()
    {
        StartCoroutine(Countdown());
    }
    private void SpawnBlocks()
    {
        //if multiple variations are there to spawn choose one randomly
        if (blocks.Length > 1)
        {
            var i = Random.Range(0, blocks.Length);
            block = blocks[i];
        }
        else if (blocks.Length == 1)
        {
            block = blocks[0];
        }
        else
        {
            Debug.LogWarning(gameObject.name+ " needs assigned prefab to spawn");
            return;
        }
        int rand = Random.Range(0, spawnChance);
        if(rand == 0)
        {
            Vector2 origin = transform.position;
            var spawnPoint = RandomPointInAnnulus(origin, minSpawnDistance, maxSpawnDistance);
            
            //float randPosX = Random.Range(-randomPos, randomPos);
            //float randPosY = Random.Range(-randomPos, randomPos);
            float randPosZ = Random.Range(-randomPosZ, randomPosZ);
            var spawnPos = new Vector3(transform.position.x + spawnPoint.x,transform.position.y + spawnPoint.y, transform.position.z + randPosZ);

            float randRotaX = Random.Range(-randomRotation, randomRotation);
            float randRotaY = Random.Range(-randomRotation, randomRotation);
            float randRotaZ = Random.Range(-randomRotation, randomRotation);
            var spawnTorque = new Vector3(randRotaX, randRotaY, randRotaZ);
            float randTorqueForce = Random.Range(-randomTorque, randomTorque);

            GameObject go = Instantiate(block, spawnPos, Quaternion.identity);

            float randomScale = Random.Range(randomMinSize, randomMaxSize);
            go.transform.localScale = new Vector3(transform.localScale.x * randomScale, transform.localScale.x * randomScale, transform.localScale.x * randomScale);

            go.GetComponent<Rigidbody>().AddTorque(spawnTorque * randTorqueForce);

            go.GetComponent<Rigidbody>().velocity = Random.onUnitSphere * Random.Range(randomSpeedMin, randomSpeedMax);

            //go.transform.SetParent(gameObject.transform);
        }
    }

    private IEnumerator Countdown()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(secondsBetweenSpawns);
            SpawnBlocks();
        }
        

    }

    public Vector2 RandomPointInAnnulus(Vector2 origin, float minRadius, float maxRadius)
    {
       

        Vector2 point = Random.insideUnitCircle.normalized * Random.Range(minRadius, maxRadius);
        

        return point;
    }
}
