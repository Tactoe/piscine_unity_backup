using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour {

    public GameObject[] cubes;

    private float[] timer;
    private GameObject[] spawnedItems;

    void Start () {
        timer = new float[3];
        for (int i = 0; i < timer.Length; i++)
            timer[i] = Random.Range(1, 4);
        spawnedItems = new GameObject[3];
	}
	
	void Update () {
        for (int i = 0; i < timer.Length; i++)
        {
            timer[i] -= Time.deltaTime;
            if (timer[i] <= 0)
            {
                if (spawnedItems[i] == null)
                    spawnedItems[i] = Instantiate(cubes[i]);
                timer[i] = Random.Range(1, 4);
            }
        }

	}
}
