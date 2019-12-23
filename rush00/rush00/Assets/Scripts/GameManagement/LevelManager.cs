using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    //public GameObject Exit;
    public static bool exitHasBeenReached;


    // Start is called before the first frame update
    void Start()
    {
        exitHasBeenReached = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (exitHasBeenReached)
        {
            Debug.Log("You found the exit !");
            GameManager.gm.LevelCompleted();
        }
    }
}
