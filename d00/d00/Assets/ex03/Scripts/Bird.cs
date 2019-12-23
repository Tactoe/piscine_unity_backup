using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    public float weight;
    public float jumpHeight;
    public Transform pipePosition;

    private float goalHeight;
    private bool isFalling;
    private bool gameGoingOn;

	void Start () {
        gameGoingOn = true;
        goalHeight = transform.position.y;
        isFalling = true;
	}
	
	void Update () {
        if (gameGoingOn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                goalHeight = transform.position.y + jumpHeight;
                isFalling = false;
            }
            if (!isFalling && transform.position.y < goalHeight)
                transform.Translate(0, jumpHeight / 3, 0);
            else
                isFalling = true;
            if (isFalling)
                transform.Translate(0, -weight * Time.deltaTime, 0);
        }

        if (transform.position.y < -3.45
            || (pipePosition.position.x < 1 && pipePosition.position.x > -1
                && (transform.position.y > 1.5 || transform.position.y < -1.5)))
            gameGoingOn = false;
    }
}
