using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gm;
    public AudioSource normalMusic;
    public AudioSource panicMusic;
    public AudioSource alarm;
    Player player;

    private void Awake()
    {
        if (gm == null)
            gm = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        normalMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (normalMusic.isPlaying && player.ennemyDetectionAmount > 75)
        {
            alarm.Play();
            normalMusic.Stop();
            panicMusic.Play();
        }
        if (panicMusic.isPlaying && player.ennemyDetectionAmount < 75)
        {
            if (alarm.isPlaying)
                alarm.Stop();
            panicMusic.Stop();
            normalMusic.Play();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
