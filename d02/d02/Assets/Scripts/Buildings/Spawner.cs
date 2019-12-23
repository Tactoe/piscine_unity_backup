using UnityEngine;
using System.Collections;

public class Spawner : Building
{

    public float spawnDelay;
    public GameObject unitToSpawn;
    public Vector3 spawnPosition;

    float spawnTimer;
    void SpawnUnit()
    {
        GameObject tmp = Instantiate(unitToSpawn);
        tmp.transform.position = Vector2.one * spawnPosition;
        tmp.transform.position += new Vector3(Random.Range(0.1f, 0.8f), Random.Range(0.1f, 0.8f));
    }

    public override void Start()
    {
        base.Start();
        spawnTimer = spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer <= 0)
        {
            SpawnUnit();
            spawnTimer = spawnDelay;
        }
        else
            spawnTimer -= Time.deltaTime;
        if (health <= 0)
        {
            dying = true;
            Debug.Log(gameObject.tag + " lost!");
            SoundManager.instance.PlaySound(deathSound);
            Destroy(gameObject);
        }
    }
}
