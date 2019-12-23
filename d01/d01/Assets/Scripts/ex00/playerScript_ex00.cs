using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class playerScript_ex00 : MonoBehaviour
{
    public float speed = 10;
    public float maxSpeed = 10;
    public float jumpHeight = 300;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.5f;
    public bool isActive;

    bool grounded;
    Rigidbody2D playerRB;

    void CapVelocity() {
        float cappedXVelocity = Mathf.Min(Mathf.Abs(playerRB.velocity.x), maxSpeed) * Mathf.Sign(playerRB.velocity.x);

        playerRB.velocity = new Vector2(cappedXVelocity, playerRB.velocity.y);
    }

    public void UpdateState() {
        if (!isActive && grounded)
            playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        if (isActive)
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void HandleInput() {
        float xInput = Input.GetAxis("Horizontal");

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void BetterGravity() {
        if (playerRB.velocity.y < 0)
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        if (playerRB.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    }

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        grounded = false;
    }

    void Update()
    {
        BetterGravity();
        if (isActive)
            HandleInput();
        UpdateState();
    }

    private void FixedUpdate()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!grounded && Mathf.RoundToInt(playerRB.velocity.y) == 0)
            grounded = true;
    }
}
