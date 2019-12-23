using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Weapon : MonoBehaviour
{

    //public int damage;
    public float shakeAmount;
    public List<string> dealsDamageTo;
    public float attackDelay;
    public bool canHoldMouse;
    public Transform target;
    public GameObject user;
    protected Rigidbody2D rb;
    protected float attackTimer;
    protected AudioSource[] audioSrc;

    public virtual void Attack()
    {
        audioSrc[Random.Range(0, audioSrc.Length)].Play();
        attackTimer = attackDelay;
    }

    public virtual bool CanAttack()
    {
        return true;
    }

    public void Throw()
    {
        Vector2 direction = target.position - transform.position;
        float dist = 25;
        direction.Normalize();
        rb.AddForce(new Vector2(direction.x * dist, direction.y * dist), ForceMode2D.Impulse);
    }

    public virtual void Awake()
    {
        audioSrc = GetComponents<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.5f)
            rb.velocity -= Mathf.Sign(rb.velocity.x) * Vector2.right * 10 * Time.deltaTime;
        else
            rb.velocity = Vector2.up * rb.velocity;
        if (Mathf.Abs(rb.velocity.y) > 0.5f)
            rb.velocity -= Mathf.Sign(rb.velocity.y) * Vector2.up * 10 * Time.deltaTime;
        else
            rb.velocity = Vector2.right * rb.velocity;

        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

    }
}
