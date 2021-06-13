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
    public GameObject block;
    [SerializeField] [Range(0,300)] int spawnChance = 64;
    //public float elapsedTime = 0.0f;
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
            Vector2 origin = transform.position;
            var spawnPoint = RandomPointInAnnulus(origin, minSpawnDistance, maxSpawnDistance);
            
            //float randPosX = Random.Range(-randomPos, randomPos);
            //float randPosY = Random.Range(-randomPos, randomPos);
            float randPosZ = Random.Range(-randomPosZ, randomPosZ);
            var spawnPos = new Vector3(spawnPoint.x,spawnPoint.y, transform.position.z + randPosZ);

            float randRotaX = Random.Range(-randomRotation, randomRotation);
            float randRotaY = Random.Range(-randomRotation, randomRotation);
            float randRotaZ = Random.Range(-randomRotation, randomRotation);
            var spawnTorque = new Vector3(randRotaX, randRotaY, randRotaZ);
            float randTorqueForce = Random.Range(-randomTorque, randomTorque);

            GameObject go = Instantiate(block, spawnPos, Quaternion.identity);

            go.GetComponent<Rigidbody>().AddTorque(spawnTorque * randTorqueForce);
            //go.transform.SetParent(gameObject.transform);
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

    public Vector2 RandomPointInAnnulus(Vector2 origin, float minRadius, float maxRadius)
    {
        /*
        var randomDirection = (Random.insideUnitCircle * origin).normalized;
        Debug.Log(randomDirection);

        var randomDistance = Random.Range(minRadius, maxRadius);

        var point = origin + randomDirection * randomDistance;
        */

        Vector2 point = Random.insideUnitCircle.normalized * Random.Range(minRadius, maxRadius);
        Debug.Log(point);

        return point;
    }
}
