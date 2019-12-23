using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGameValues : MonoBehaviour
{
    Text energy;
    Text health;
    // Start is called before the first frame update
    void Start()
    {
        Text[] gameTexts = GetComponentsInChildren<Text>();
        foreach (Text text in gameTexts)
        {
            if (text.gameObject.name == "Energy")
                energy = text;
            else if (text.gameObject.name == "Health")
                health = text;
        }
        health.text = gameManager.gm.playerMaxHp.ToString();
        energy.text = gameManager.gm.playerStartEnergy.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = gameManager.gm.playerHp.ToString();
        energy.text = gameManager.gm.playerEnergy.ToString();
    }
}
