using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public Player player;
    public Text healthT;
    public Text levelT;
    public Text expT;
    public Image healthBar;
    public Image expBar;
    public GameObject gameOverScreen;

    float maxHealthLength;
    float maxExpLength;
    float healthLength;
    float expLength;
    void Start()
    {
        maxHealthLength = healthBar.rectTransform.sizeDelta.x;
        maxExpLength = expBar.rectTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        healthT.text = "Health: " + player.health;
        levelT.text = "Level " + player.level;
        expT.text = "Exp: " + player.xp + " / " + player.xpForNextLevel;
        healthLength = (float)player.health / player.maxHealth * maxHealthLength;
        expLength = (float)player.xp / player.xpForNextLevel * maxExpLength;
        healthBar.rectTransform.sizeDelta = new Vector2 (healthLength, healthBar.rectTransform.sizeDelta.y);
        expBar.rectTransform.sizeDelta = new Vector2(expLength, expBar.rectTransform.sizeDelta.y);
        if (player.isDead && !gameOverScreen.activeSelf)
        {
            gameOverScreen.SetActive(true);
        }
    }
}
