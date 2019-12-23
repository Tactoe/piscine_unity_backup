using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public Player player;
    public Text healthT;
    public Text timeText;
    public Text waveAmount;
    public Image healthBar;
    public GameObject gameOverScreen;

    float maxHealthLength;
    float healthLength;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        maxHealthLength = healthBar.rectTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = "Time remaining before " + (GameManager.gm.isRestingWave ? "next wave" : "pause wave") + ":\n" + Mathf.RoundToInt(GameManager.gm.timer);
        healthT.text = "Health: " + player.health;
        healthLength = (float)player.health / player.maxHealth * maxHealthLength;
        healthBar.rectTransform.sizeDelta = new Vector2 (healthLength, healthBar.rectTransform.sizeDelta.y);
        if (player.isDead && !gameOverScreen.activeSelf)
        {
            gameOverScreen.SetActive(true);
            waveAmount.text = "Waves survived: " + GameManager.gm.currentWave;
        }
    }
}
