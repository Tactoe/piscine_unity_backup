using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {
    public float speed;
    public float speedIncrease;
    public Transform bird;

    private int score;
    private float timer;
    private bool gameGoingOn;

	void Start () {
        score = 0;
        timer = 0;
        gameGoingOn = true;
	}
	
	void Update () {
        if (transform.position.x < -10)
        {
            speed += speedIncrease;
            score += 5;
            transform.position = new Vector3(10, 0, 0);
        }
        if (gameGoingOn)
        {
            timer += Time.deltaTime;
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }

        if (gameGoingOn && (bird.position.y < -3.45
            || (transform.position.x < 1 && transform.position.x > -1
                && (bird.position.y > 1.5 || bird.position.y < -1.5))))
        {
            Debug.Log("Score: " + score
                      + "\nTime: " + Mathf.RoundToInt(timer) + "s");
            gameGoingOn = false;
        }
    }
}
