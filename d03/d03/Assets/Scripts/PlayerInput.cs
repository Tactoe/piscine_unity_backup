using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject pauseMenu;
    bool isPaused;
    // Start is called before the first frame update

    public void SwitchGamePauseState()
    {
        isPaused = !isPaused;
        gameManager.gm.pause(isPaused);
        pauseMenu.SetActive(isPaused);
    }

    void Start()
    {
        pauseMenu.SetActive(isPaused);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameManager.gm.gameGoingOn)
            SwitchGamePauseState();
    }
}
