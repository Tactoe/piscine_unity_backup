using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class gameManager_ex01 : MonoBehaviour
{

    public GameObject[] characters;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bool winCondition = true;
        for (int i = 0; i < characters.Length; i++)
        {
            if (!characters[i].GetComponent<playerScript_ex01>().isAtGoal)
            {
                winCondition = false;
                break;
            }
        }
        if (winCondition || Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("You won!");
            if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
