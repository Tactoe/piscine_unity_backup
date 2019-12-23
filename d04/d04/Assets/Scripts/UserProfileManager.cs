using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UserProfileManager : MonoBehaviour
{
    public static UserProfileManager upm;

    private void Awake()
    {
        if (upm == null)
            upm = this;
        profile = GetUserProfile();
    }

    public UserProfile profile;
    string[] levelList = { "AngelIsland", "OilOcean" };

    public void ResetUserProfile()
    {
        PlayerPrefs.SetInt("ringTotal", 0);
        PlayerPrefs.SetInt("liveLost", 0);
        PlayerPrefs.SetString("levelsTime", "-1|-1");
        PlayerPrefs.SetString("levelsUnlocked", levelList[0]);
        PlayerPrefs.Save();
    }

    public string GetLevelBestTime(string levelName)
    {
        string str;
        int level;
        float timer;

        if (levelName == "AngelIsland")
            level = 0;
        else if (levelName == "OilOcean")
            level = 1;
        else
            return "-:--";
        if (level >= profile.levelsTime.Count)
            return "-:--";
        timer = profile.levelsTime[level];
        if (timer == -1)
            return "-:--";
        str = Mathf.Floor(timer / 60) + ":" + (timer % 60 < 10 ? "0" : "") + Mathf.Floor(timer % 60);
        return str;
    }

    public void UpdateUserProfile()
    {
        PlayerPrefs.SetInt("ringTotal", profile.ringTotal);
        PlayerPrefs.SetInt("liveLost", profile.livesLost);
        string levelTimeString = "";
        for (int i = 0; i < profile.levelsTime.Count; i++)
            levelTimeString += profile.levelsTime[i] + "|";
        levelTimeString = levelTimeString.TrimEnd('|');
        PlayerPrefs.SetString("levelsTime", levelTimeString);
        string levelString = "";
        for (int i = 0; i < profile.levelsUnlocked.Count; i++)
            levelString += profile.levelsUnlocked[i] + "|";
        levelString = levelString.TrimEnd('|');
        Debug.Log(levelString);
        PlayerPrefs.SetString("levelsUnlocked", levelString);
        PlayerPrefs.Save();
    }

    public UserProfile GetUserProfile()
    {
        UserProfile tmp;
        if (!PlayerPrefs.HasKey("levelsUnlocked"))
            ResetUserProfile();
        tmp.ringTotal = PlayerPrefs.GetInt("ringTotal");
        tmp.livesLost = PlayerPrefs.GetInt("livesLost");
        tmp.levelsUnlocked = PlayerPrefs.GetString("levelsUnlocked").Split('|').ToList();
        tmp.levelsTime = new List<int>();
        Debug.Log(PlayerPrefs.GetString("levelsTime"));
        string[] allTimes = PlayerPrefs.GetString("levelsTime").Split('|');
        Debug.Log(allTimes);
        foreach (string time in allTimes)
        {
            tmp.levelsTime.Add(int.Parse(time));
        }
        return tmp;
    }
}

