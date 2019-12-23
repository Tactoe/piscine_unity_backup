using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public GameObject[] platforms;
    public GameObject[] blocs;

    // Start is called before the first frame update
    int blocIndex;
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
        if (collision.gameObject.CompareTag("red"))
        {
            blocIndex = 1;
            colorIndicator.color = Color.red;
        }
        else if (collision.gameObject.CompareTag("blue"))
        {
            blocIndex = 2;
            colorIndicator.color = Color.blue;
        }
        else if (collision.gameObject.CompareTag("yellow"))
        {
            blocIndex = 3;
            colorIndicator.color = Color.yellow;
        }
        for (int i = 0; i < platforms.Length;i++)
        {
            GameObject tmp = Instantiate(blocs[blocIndex]);
            tmp.transform.position = platforms[i].transform.position;
            Destroy(platforms[i]);
            platforms[i] = tmp;
        }
    }
}
