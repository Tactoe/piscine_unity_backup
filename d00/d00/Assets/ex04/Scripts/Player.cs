using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode upKey;
    public KeyCode downKey;

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(upKey) && transform.position.y < 3.85)
            transform.Translate(0, speed * Time.deltaTime, 0);
        if (Input.GetKey(downKey) && transform.position.y > -3.85)
            transform.Translate(0, -speed * Time.deltaTime, 0);
    }
}
