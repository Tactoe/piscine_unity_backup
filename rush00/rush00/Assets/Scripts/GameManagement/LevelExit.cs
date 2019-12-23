using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : LevelManager
{
    //[HideInInspector]public bool hasBeenReached = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            exitHasBeenReached = true;
    }
}
