using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float attackRange;
    public float attackDelay;
    public int strength;
    public int maxHealth;
    public AudioClip startWalkingSound;
    public AudioClip attackSound;
    public AudioClip deathSound;
    [HideInInspector] public int health;
    [HideInInspector]public bool dying;

    protected Vector3 target;
    protected string opponentTag;
    protected Animator animator;
    protected AudioSource audioSrc;

    float attackTimer;
    [SerializeField] GameObject attackTarget;

    public void SetNewTargetDirection(Vector3 newTarget)
    {
        attackTarget = null;
        target = newTarget;
        target.z = transform.position.z;
        transform.up = -(target - transform.position);
        animator.SetBool("isWalking", true);
        PlaySound(startWalkingSound);
    }

    void PlaySound(AudioClip audioToPlay)
    {
        audioSrc.clip = audioToPlay;
        audioSrc.Play();
    }

    void Attack()
    {
        if (attackTarget.GetComponent<Unit>())
        {
            Unit tmp = attackTarget.GetComponent<Unit>();
            if (tmp.dying)
                return;
            tmp.health -= strength;
            Debug.Log(opponentTag + " Unit [" + tmp.health + "/" + tmp.maxHealth + "]HP has been attacked.");
        }
        else if (attackTarget.GetComponent<Building>())
        {
            Building tmp = attackTarget.GetComponent<Building>();
            if (tmp.dying)
                return;
            tmp.health -= strength;
            Debug.Log(opponentTag + " Building [" + tmp.health + "/" + tmp.maxHealth + "]HP has been attacked.");
        }
        transform.up = Vector2.one * -(attackTarget.transform.position - transform.position);
        animator.SetTrigger("attack");
        PlaySound(attackSound);
    }

    protected void UnitBehaviorLoop()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if ((target - transform.position).magnitude < 0.1f)
        {
            animator.SetBool("isWalking", false);
            LookForNearbyStuffToAttack();
        }
        if (attackTarget != null)
        {
            if (Vector2.Distance(transform.position, attackTarget.transform.position) <= attackRange)
            {
                if (attackTimer < 0)
                {
                    Attack();
                    attackTimer = attackDelay;
                }
                else
                    attackTimer -= Time.deltaTime;
            }
            else 
                SetNewTargetDirection(attackTarget.transform.position);
        }
        if (health <= 0)
        {
            dying = true;
            animator.SetTrigger("die");
            PlaySound(deathSound);
            Destroy(gameObject, deathSound.length);
        }
    }

    void LookForNearbyStuffToAttack()
    {
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D obj in nearbyObjects)
        {
            if (obj.transform == transform)
                continue;
            if (obj.gameObject.CompareTag(opponentTag)
                && Vector2.Distance(transform.position, obj.transform.position) <= attackRange)
            {
                if (obj.gameObject.GetComponent<Unit>() != null)
                    attackTarget = obj.gameObject;
                else if (obj.gameObject.GetComponent<Building>() != null)
                    attackTarget = obj.gameObject;
                break;
            }
        }
    }

    public virtual void Start()
    {
        health = maxHealth;
        animator = gameObject.GetComponent<Animator>();
        audioSrc = gameObject.GetComponent<AudioSource>();
        target = transform.position;
    }

    void Update()
    {
        if (!dying)
            UnitBehaviorLoop();
    }
}
