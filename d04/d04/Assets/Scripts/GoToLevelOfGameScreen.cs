using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToLevelOfGameScreen : MonoBehaviour
{
    Button toClick;
    Text bestTimeTxt;
    // Start is called before the first frame update
    void Start()
    {
        toClick = GetComponent<Button>();
        toClick.onClick.AddListener(LoadAssignedLevel);
        Text[] tmp = gameObject.GetComponentsInChildren<Text>();
        foreach (Text txt in tmp)
        {
            if (txt.gameObject.CompareTag("bestTime"))
                txt.text = UserProfileManager.upm.GetLevelBestTime(gameObject.name);
        }
    }

    void OnDestroy()
    {
        toClick.onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadAssignedLevel()
    {
        if (UserProfileManager.upm.profile.levelsUnlocked.Contains(gameObject.name))
            GameManager.gm.LoadLevelByName(gameObject.name);
    }
}
