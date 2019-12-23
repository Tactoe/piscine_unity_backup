using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    public float attackDuration;
    public float thrustAmount;
    Animator anim;
    BoxCollider2D hitBox;
    Rigidbody2D userRb2d;

    public override bool CanAttack()
    {
        return (attackTimer <= 0);
    }

    public override void Attack()
    {
        if (userRb2d == null)
            userRb2d = user.transform.GetComponent<Rigidbody2D>();
        Vector2 dir = (Vector2)(transform.position - user.transform.position);
        dir.Normalize();
        userRb2d.AddForce(dir * thrustAmount, ForceMode2D.Impulse);
        hitBox.enabled = true;
        anim.SetTrigger("swing");
        Invoke("DisableHitbox", attackDuration);
        base.Attack();
    }

    void DisableHitbox()
    {
        userRb2d.velocity = Vector2.zero;
        hitBox.enabled = false;
    }

    public override void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        hitBox = GetComponent<BoxCollider2D>();
        hitBox.enabled = false;
        base.Awake();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dealsDamageTo.Contains(collision.gameObject.tag))
        {
            collision.gameObject.GetComponent<Character>().Die();
        }
    }
}
