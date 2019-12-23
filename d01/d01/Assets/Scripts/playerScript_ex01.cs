using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class playerScript_ex01 : MonoBehaviour
{
    public float speed = 1;
    public float maxSpeed = 10;
    public float jumpHeight = 1;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.5f;
    public bool isActive;
    public bool isAtGoal;

    bool grounded;
    Rigidbody2D playerRB;

    void CapVelocity()
    {
        float cappedXVelocity = Mathf.Min(Mathf.Abs(playerRB.velocity.x), maxSpeed)
                                     * Mathf.Sign(playerRB.velocity.x);
        playerRB.velocity = new Vector2(cappedXVelocity, playerRB.velocity.y);
    }

    public void UpdateState()
    {
        if (!isActive && grounded)
            playerRB.constraints = RigidbodyConstraints2D.FreezePositionX
                | RigidbodyConstraints2D.FreezeRotation;
        if (isActive)
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void HandleInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        float xForce = xInput * speed * Time.deltaTime;
        float yForce = 0;
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            yForce = jumpHeight * Time.deltaTime;
            grounded = false;
        }
        playerRB.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        if (playerRB.velocity.x > maxSpeed)
            CapVelocity();
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Backspace))
            SceneManager.LoadScene(0);
    }

    void SmoothMove()
    {
        if (playerRB.velocity.y < 0)
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        if (playerRB.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            playerRB.velocity = new Vector2(playerRB.velocity.x * 0.8f, playerRB.velocity.y);
    }

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        grounded = false;
    }

    void Update()
    {
        SmoothMove();
        if (isActive)
            HandleInput();
        UpdateState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!grounded && Mathf.RoundToInt(playerRB.velocity.y) == 0)
            grounded = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == gameObject.tag)
            isAtGoal = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isAtGoal = false;
    }
}
