using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;


    public float timer;
    public int score;
    public bool gameGoingOn;
    private void Awake()
    {
        if (gm == null)
            gm = this;
    }

    public void Start()
    {
        if (gameGoingOn)
        {
            if (!UserProfileManager.upm.profile.levelsUnlocked.Contains(SceneManager.GetActiveScene().name))
                UserProfileManager.upm.profile.levelsUnlocked.Add(SceneManager.GetActiveScene().name);
            UserProfileManager.upm.UpdateUserProfile();
        }
    }

    public int GetCurrentSceneTimeIndex()
    {
        string levelName = SceneManager.GetActiveScene().name;
        if (levelName == "AngelIsland")
            return 0;
        if (levelName == "OilOcean")
            return 1;
        return -1;
    }

    public void NextLevel()
    {
        UserProfileManager.upm.UpdateUserProfile();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(sceneIndex + 1);
        else
            SceneManager.LoadScene(0);
    }

    public void LoadLevelByName(string levelName)
    {
        if (SceneManager.GetSceneByName(levelName) != null)
            SceneManager.LoadScene(levelName);
    }

    void Update()
    {
        if (gameGoingOn)
        {
            timer += Time.deltaTime;
        }
    }
}
