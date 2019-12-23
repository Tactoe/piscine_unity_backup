using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CheckPlayerProgression : MonoBehaviour
{
    public GameObject playerProgress;

    UserProfile profile;
    GameObject[] levelScreens;

    void Start()
    {
        profile = UserProfileManager.upm.profile;
        levelScreens = GameObject.FindGameObjectsWithTag("levelScreen");
        Text[] tmp = playerProgress.GetComponentsInChildren<Text>();
        foreach (Text txt in tmp)
        {
            if (txt.name == "ringTotal")
                txt.text = "Ring total is " + profile.ringTotal;
            else if (txt.name == "livesLost")
                txt.text = "Total lives lost is " + profile.livesLost;
        }
        foreach (GameObject level in levelScreens)
        {
            if (profile.levelsUnlocked.Contains(level.name))
                level.GetComponentInChildren<Image>().color = new Color(1, 1, 1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
