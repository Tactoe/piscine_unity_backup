using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{

    public GameObject[] ennemyToSpawn;
    public float minSpawnDelay;
    public float maxSpawnDelay;

    GameObject currentSpawned;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnnemy()
    {
        while (true)
        {
            if (currentSpawned == null)
            {
                currentSpawned = Instantiate(ennemyToSpawn[Random.Range(0, ennemyToSpawn.Length)]);
                currentSpawned.transform.position = transform.position;
            }
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
