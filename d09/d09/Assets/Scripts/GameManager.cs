using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public Player player;
    public int hpIncreasePerWave;
    public int damageIncreasePerWave;
    //public int waveAmount;
    public int hpBoost = 0;
    public int damageBoost = 0;
    public float waveDuration;
    public float restingWaveDuration;
    public bool isRestingWave;
    public int currentWave;
    public float timer;

    public List<GameObject> ennemies = new List<GameObject>();
    private void Awake()
    {
        if (gm == null)
            gm = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = waveDuration;
        StartCoroutine(EmptyEnnemies());
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            if (!player.isDead)
            timer -= Time.deltaTime;
        }
        else if (!isRestingWave)
        {
            damageBoost += damageIncreasePerWave;
            hpBoost += hpIncreasePerWave;
            currentWave++;
            timer = restingWaveDuration;
            isRestingWave = true;
        }
        else
        {
            timer = waveDuration;
            isRestingWave = false;
        }
    }
    IEnumerator EmptyEnnemies()
    {
        while (true)
        {
            ennemies.RemoveAll(item => item == null);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
