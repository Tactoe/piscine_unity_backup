using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour {

    public float stepBackSpd;
    public float stepBackMaxDist;
    public Transform ballPosition;
    public Transform goalPosition;

    private int dir;
    private bool readyToFire;
    private bool ballIsMoving;
    private float lastBallPosition;
    private float stepBackDistance;

    bool NearlyEqual(float a, float b) {
        return a > b - 0.001 && a < b + 0.001;
    }

    void CheckBallStatus () {
        //if (ballPosition != null)
        if (ballPosition.position.x > -413)
        {
            if (NearlyEqual(lastBallPosition, ballPosition.position.y))
                ballIsMoving = false;
            else
            {
                lastBallPosition = ballPosition.position.y;
                ballIsMoving = true;
            }
        }
    }

    void FollowBall() {
        transform.position = ballPosition.position;
        if (dir < 0 && transform.localScale.y > 0)
            transform.localScale = new Vector3(transform.localScale.x, -0.5f, 0);
        else if (dir > 0)
            transform.localScale = new Vector3(transform.localScale.x, 0.5f, 0);
        transform.position -= new Vector3(0.15f, dir * 0.1f, 0);
    }

    void Start () {
        readyToFire = false;
        ballIsMoving = false;
        stepBackDistance = 1f;
        lastBallPosition = 0;
        dir = 1;
	}
	
	void Update () {
        CheckBallStatus();
        dir = transform.position.y > goalPosition.position.y ? -1 : 1;
        if (Input.GetKey(KeyCode.Space) && !ballIsMoving)
        {
            if (stepBackDistance < stepBackMaxDist)
            {
                stepBackDistance += stepBackSpd * Time.deltaTime;
                transform.Translate(0, -stepBackSpd * dir * Time.deltaTime, 0);
            }
            readyToFire = true;
        }
        else if (readyToFire && stepBackDistance > 1)
        {
            transform.Translate(0, stepBackDistance / 2  * dir, 0);
            stepBackDistance = stepBackDistance / 2;
        }
        else if (readyToFire)
        {
            stepBackDistance = 1f;
            readyToFire = false;
        }
        else if (ballPosition != null)
            FollowBall();
    }
}
