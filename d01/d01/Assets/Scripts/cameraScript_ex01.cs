using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript_ex01 : MonoBehaviour
{
    public GameObject[] characters;
    public int focusIndex;

    private Transform focus;

    void FollowFocus() {
        transform.position = new Vector3(focus.position.x, focus.position.y, transform.position.z);
    }

    void SwitchFocus(int newFocus) {
        characters[focusIndex].GetComponent<playerScript_ex01>().isActive = false;
        characters[newFocus].GetComponent<playerScript_ex01>().isActive = true;
        focus = characters[newFocus].transform;
        focusIndex = newFocus;
    }

    void Start()
    {
        focusIndex = 0;
        SwitchFocus(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchFocus(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchFocus(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchFocus(2);
        FollowFocus();
    }
}
