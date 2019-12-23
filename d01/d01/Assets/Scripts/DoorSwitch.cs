using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{

    public GameObject[] doors;
    public bool openForever;

    // Start is called before the first frame update
    int blocIndex;
    int blocsCurrentlyOnTrigger;
    SpriteRenderer colorIndicator;
    void Start()
    {
        colorIndicator = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        blocsCurrentlyOnTrigger++;
        colorIndicator.color = Color.green;
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        blocsCurrentlyOnTrigger--;
        if (!openForever && blocsCurrentlyOnTrigger == 0)
        {
            colorIndicator.color = Color.white;
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].SetActive(true);
            }
        }
    }
}
