using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{

    public GameObject[] ennemyToSpawn;
    public float minSpawnDelay;
    public float maxSpawnDelay;
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
            if (GameManager.gm.ennemies.Count < 20 && !GameManager.gm.isRestingWave)
            {
                GameObject tmp = Instantiate(ennemyToSpawn[Random.Range(0, ennemyToSpawn.Length)], transform.position, transform.rotation);
                tmp.GetComponent<Ennemy>().health += GameManager.gm.hpBoost;
                tmp.GetComponent<Ennemy>().damage += GameManager.gm.damageBoost;
                GameManager.gm.ennemies.Add(tmp);
            }
            float finalMinSpawnDelay = minSpawnDelay - GameManager.gm.currentWave;
            float finalMaxSpawnDelay = maxSpawnDelay - GameManager.gm.currentWave;
            if (finalMinSpawnDelay < 2)
                finalMinSpawnDelay = 2;
            if (finalMaxSpawnDelay < 5)
                finalMaxSpawnDelay = 5;
            yield return new WaitForSeconds(Random.Range(finalMinSpawnDelay, finalMaxSpawnDelay));
        }
    }
}
