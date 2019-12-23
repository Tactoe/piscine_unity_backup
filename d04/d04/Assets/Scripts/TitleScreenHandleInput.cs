using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenHandleInput : MonoBehaviour
{
    Button resetButton;
    // Start is called before the first frame update
    void Start()
    {
        resetButton = GameObject.FindWithTag("resetButton").GetComponent<Button>();
        resetButton.onClick.AddListener(ResetPlayerProgress);
    }

    public void ResetPlayerProgress()
    {
        UserProfileManager.upm.ResetUserProfile();
    }

    private void OnDestroy()
    {
        resetButton.onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            GameManager.gm.LoadLevelByName("DataSelect");
    }
}
