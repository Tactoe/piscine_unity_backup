using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCamera : MonoBehaviour
{
    public float moveSpeed;
    public float speedH, speedV;

    Transform freeViewTarget;
    Transform ball;
    Rigidbody rb;
    BoxCollider box;
    float yaw, pitch;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        ball = GameObject.FindWithTag("Ball").transform;
        box = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    void HandleInputFreeView()
    {
        float speed = moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Q))
            rb.AddForce(transform.up * speed, ForceMode.VelocityChange);
        else if (Input.GetKey(KeyCode.E))
            rb.AddForce(transform.up * -speed, ForceMode.VelocityChange);
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(transform.right * -speed, ForceMode.VelocityChange);
        else if (Input.GetKey(KeyCode.D))
            rb.AddForce(transform.right * speed, ForceMode.VelocityChange);
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        else if (Input.GetKey(KeyCode.S))
            rb.AddForce(transform.forward * -speed, ForceMode.VelocityChange);
        if (!Input.anyKey)
            rb.velocity = Vector3.zero;
        //rb.velocity = -t
        //transform.position -= transform.forward * speed;
        //transform.Translate(-transform.forward * speed);
        //transform.Translate(0, 0, -speed);
    }

    void Update()
    {
        if (GameManager.gm.isInFreeView)
        {
            if (!box.enabled)
                box.enabled = true;
            HandleInputFreeView();
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0);
        }
        else 
        {
            if (box.enabled)
            {
                box.enabled = false;
                rb.velocity = Vector3.zero;
            }
            transform.LookAt(ball);
        }
    }
}
