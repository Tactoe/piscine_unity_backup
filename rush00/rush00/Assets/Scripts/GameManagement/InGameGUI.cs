using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameGUI : MonoBehaviour
{

    Text ammoTxt;
    GameObject player;
    Character c;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        c = GameObject.FindWithTag("Player").GetComponent<Character>();
        ammoTxt = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (c.weapon != null)
        {
            if (c.weapon.GetType() == typeof(Gun))
                ammoTxt.text = "Ammo: " + ((Gun)c.weapon).ammo;
            else
                ammoTxt.text = "Infinite";
        }
        else
            ammoTxt.text = "No weapon";


    }
}
