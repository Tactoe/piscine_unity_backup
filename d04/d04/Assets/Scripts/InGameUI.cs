using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    GameObject sonicGO;
    Sonic sonic;
    Text ringTxt;
    Text timerTxt;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        sonicGO = GameObject.FindWithTag("Player");
        sonic = sonicGO.GetComponent<Sonic>();
        Text[] tmp = gameObject.GetComponentsInChildren<Text>();
        ringTxt = tmp[0];
        timerTxt = tmp[1];
    }

    // Update is called once per frame
    void Update()
    {
        timer = GameManager.gm.timer;
        timerTxt.text = "Time: " + Mathf.Floor(timer / 60) + ":" + (timer % 60 < 10 ? "0" : "") + Mathf.Floor(timer % 60);
        ringTxt.text = "Rings: " + sonic.rings;
    }
}
