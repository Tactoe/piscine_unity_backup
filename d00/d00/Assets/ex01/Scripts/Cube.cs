using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    public float goalY;
    public KeyCode cubeInput;

    private float speed;
    private float precision;

    void Start()
    {
        speed = Random.Range(2, 6);
    }

    void Update()
    {
        if (Input.GetKeyDown(cubeInput) || this.transform.position.y < -5.5)
        {
            if (Input.GetKeyDown(cubeInput))
            {
                precision = this.transform.position.y - goalY;
                //precision = precision < 0 ? precision * - 1 : precision;
                //precision = 100 - (precision / 6 * 100); 
                //Debug.Log("Precision: " + precision + "%");
                Debug.Log("Precision: " + precision);
            }
            Destroy(gameObject);
        }
        this.transform.Translate(0, -speed * Time.deltaTime, 0);
    }
}
