using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag : MonoBehaviour
{
    public Text finalScoreTxt;
    AudioSource victory;
    Animator an;
    Sonic sonic;
    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();
        victory = GetComponent<AudioSource>();
    }

    void DisplayFinalScore()
    {
        finalScoreTxt.gameObject.SetActive(true);
        float lastBestTime = UserProfileManager.upm.profile.levelsTime[GameManager.gm.GetCurrentSceneTimeIndex()];
        float newTime = GameManager.gm.timer;
        if (newTime < lastBestTime)
            UserProfileManager.upm.profile.levelsTime[0] = Mathf.RoundToInt(newTime);
        int finalScore = 20000 - Mathf.RoundToInt(GameManager.gm.timer) * 100 + sonic.rings * 100;
        finalScoreTxt.text = "Score: " + finalScore;
    }

    void NextLevel()
    {
        GameManager.gm.NextLevel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sonic = collision.gameObject.GetComponent<Sonic>();
        sonic.isHit = true;
        GameManager.gm.gameGoingOn = false;
        an.SetTrigger("activated");
        MusicManager.mm.ChangeSong(victory.clip);
        Invoke("DisplayFinalScore", 6);
        Invoke("NextLevel", 10);
    }
}
