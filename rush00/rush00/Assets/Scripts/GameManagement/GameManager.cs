using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gm;
    public bool gameGoingOn { private set; get; }

    private void Awake()
    {
        if (gm == null)
            gm = this;
    }

    void Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void NextScene() {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(0);
    }

    public void RestartLevel(float time = 0)
    {
    }

    public void GameOver()
    {
        Annoucements.annoucements.Fail();
        gameGoingOn = false;
        Invoke("Restart", 5);
    }

    public void LevelCompleted() {
        Annoucements.annoucements.Success();
        gameGoingOn = false;
        Invoke("NextScene", 5);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CheckEnnemyQuantity");
        gameGoingOn = true;
    }

    IEnumerator CheckEnnemyQuantity()
    {
        int ennemyQuantity = GameObject.FindGameObjectsWithTag("Ennemy").Length;
        while (ennemyQuantity != 0)
        {
            ennemyQuantity = GameObject.FindGameObjectsWithTag("Ennemy").Length;
            yield return new WaitForSeconds(0.5f); 
        }
        LevelCompleted();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        
    }
}
