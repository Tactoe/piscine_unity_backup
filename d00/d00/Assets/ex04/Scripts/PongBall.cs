using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour
{
    public Transform rightPlayer;
    public Transform leftPlayer;
    public float maxSpeed;
    public float speedIncrease;

    private float xSpeed;
    private float ySpeed;
    private int scoreLeft;
    private int scoreRight;

    bool NearlyEqual(float a, float b, float offset) {
        return a > b - offset && a < b + offset;
    }

    void SetSpeed() {
        xSpeed = Random.Range(3, 8);
        xSpeed = Random.Range(1, 3) == 1 ? -xSpeed : xSpeed;
        ySpeed = Random.Range(2, 6);
        ySpeed = Random.Range(1, 3) == 1 ? -ySpeed : ySpeed;
    }

    bool TouchesPlayer(Transform player) {
        return NearlyEqual(transform.position.x, player.position.x, 0.125f)
                && NearlyEqual(transform.position.y, player.position.y, 1.25f);
    }

    void Start()
    {
        scoreLeft = 0;
        scoreRight = 0;
        SetSpeed();
    }

    void Update()
    {
        if (transform.position.y > 4.75 || transform.position.y < -4.75)
            ySpeed *= -1;
        if (TouchesPlayer(leftPlayer) || TouchesPlayer(rightPlayer))
        {
            if (xSpeed <= maxSpeed && xSpeed >= -maxSpeed)
                xSpeed += speedIncrease * xSpeed < 0 ? -1 : 1;
            xSpeed *= -1;
        }
        if (transform.position.x > 8 || transform.position.x < -8)
        {
            if (transform.position.x > 8)
                scoreLeft++;
            else
                scoreRight++;
            Debug.Log("Player 1: " + scoreLeft + " | Player 2: " + scoreRight);

            transform.position = new Vector3(0, 0, 0);
            SetSpeed();
        }
        transform.Translate(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0);

    }
}
