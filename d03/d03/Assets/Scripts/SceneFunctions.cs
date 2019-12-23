using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneFunctions : MonoBehaviour
{


    public void StartGame()
    {
        SceneManager.LoadScene("ex01");
    }

    public void NextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            GoToMenu();
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ex00");
    }

    public void Exit()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
