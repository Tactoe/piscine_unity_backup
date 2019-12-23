using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTransition : MonoBehaviour
{

    public GameObject victoryScreen;
    public GameObject gameOverScreen;
    bool CheckLastEnnemy()
    {
        if (gameManager.gm.lastWave == true)
        {
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("spawner");
            foreach (GameObject spawner in spawners)
            {
                if (spawner.GetComponent<ennemySpawner>().isEmpty == false || spawner.transform.childCount >= 1)
                    return false;
            }
            return true;
        }
        return false;
    }

    void Start()
    {
    }


    void Update()
    {
        if (CheckLastEnnemy())
        {
            gameManager.gm.gameGoingOn = false;
            victoryScreen.SetActive(true);
        }
        if (gameManager.gm.playerHp <= 0)
        {
            gameOverScreen.SetActive(true);
        }
        //if (gm.gameOver())
    }
}
