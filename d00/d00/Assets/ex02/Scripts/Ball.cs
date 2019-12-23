using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public float friction;
    public float maxSpeed;
    public float speedVictoryLimit;
    public Transform goalPosition;

    private int score;
    private int dir;
    private float currentSpeed;
    private bool canLaunch;
    private bool gameGoingOn;

    void Setdir() {
        if (transform.position.y < -4.6 || transform.position.y > 4.6)
            dir *= -1;
        if (currentSpeed < 0.1
            && transform.position.y < goalPosition.position.y)
            dir = 1;
    }

    void CheckVictory() {
        if (transform.position.y > goalPosition.position.y - 0.5
            && transform.position.y < goalPosition.position.y + 0.5
            && currentSpeed <= speedVictoryLimit)
        {
            if (score > 0)
                Debug.Log("You lost with " + score);
            else
                Debug.Log( "You won with " + score);
            transform.Translate(-413, -413, -413);
            gameGoingOn = false;
            //Destroy(gameObject);
        }

    }

    void Start()
    {
        score = -15;
        dir = 1;
        currentSpeed = 0;
        canLaunch = true;
        gameGoingOn = true;
    }

    void Update()
    {
        Setdir();
        if (Input.GetKey(KeyCode.Space) && canLaunch)
        {
            if (currentSpeed < maxSpeed)
                currentSpeed += (maxSpeed / 2) * Time.deltaTime;
        }
        else if (currentSpeed > 0)
        {
            canLaunch = false;
            transform.Translate(0, (currentSpeed / 10) * dir, 0);
            currentSpeed -= friction * Time.deltaTime;
        }
        else if (!canLaunch && gameGoingOn)
        {
            score += 5;
            Debug.Log("Score is:" + score);
            canLaunch = true;
        }
        CheckVictory();
    }
}
