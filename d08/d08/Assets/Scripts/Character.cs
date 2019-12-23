using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Character : MonoBehaviour
{

    public float detectionRange;
    public int strength;
    public int agility;
    public int constitution;
    public int armor;
    public int level;
    public int xp;
    public int money;
    public bool isDead { private set; get; }
    public int health { private set; get; }
    public int maxHealth;

    protected int minDamage;
    protected int maxDamage;

    protected bool loopAttack;
    protected bool canAttack;
    protected NavMeshAgent agent;
    protected Animator anim;
    protected Transform attackTarget;
    protected float attackRange;
    protected float attackTargetDist;
    // Start is called before the first frame update
    public virtual void Start()
    {
        maxHealth = constitution * 5;
        health = maxHealth;
        minDamage = strength / 2;
        maxDamage = minDamage + 4;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        attackRange = agent.stoppingDistance;
        StartCoroutine(AnimationLoop());
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (health <= 0 && !isDead)
        {
            anim.SetTrigger("die");
            isDead = true;
        }
        if (attackTarget != null)
        {
            agent.destination = attackTarget.position;
            attackTargetDist = Vector3.Distance(agent.destination, transform.position);
            anim.SetBool("isRunning", (attackTargetDist > attackRange));
            if (attackTarget.GetComponent<Character>().isDead)
                attackTarget = null;
        }
        else
            attackTargetDist = 100;
        anim.SetFloat("targetDist", attackTargetDist);
        anim.SetFloat("velx", Mathf.Abs(agent.velocity.x));
        anim.SetFloat("vely", Mathf.Abs(agent.velocity.z));
    }

    public void DealDamageToTarget()
    {
        if (attackTarget != null)
        {
            Character target = attackTarget.GetComponent<Character>();
            if (attackTarget != null && Random.Range(0, 100) < (75 + agility - target.agility))
            {
                int finalDamage = Random.Range(minDamage, maxDamage);
                finalDamage = finalDamage * (1 - target.armor / 200);
                target.health -= finalDamage;
            }

        }
    }

    IEnumerator AnimationLoop()
    {
        while (!isDead)
        {

            yield return new WaitForSeconds(0.1f);
        }
    }

    //IEnumerator AnimationLoop()
    //{
    //    while (!isDead)
    //    {
    //        if (attackTarget != null
    //            && Vector3.Distance(agent.destination, transform.position) < detectionRange
    //            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")
    //            && Vector3.Distance(agent.destination, transform.position) > attackRange)
    //        {
    //            anim.SetBool("isRunning", true);
    //        }
    //        else if (attackTarget != null
    //                 && attackTargetDist < attackRange && canAttack)
    //        {
    //            anim.SetBool("isRunning", false);
    //            anim.SetTrigger("attack");
    //            transform.LookAt(attackTarget.transform);
    //            anim.SetBool("canAttack", false);
    //        }
    //        else
    //        {
    //            anim.SetBool("isRunning", false);
    //        }
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}

    public void StartDeathCoroutine()
    {
        StartCoroutine(Die());
    }

    public virtual IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        while (anim.gameObject.transform.position.y > -3)
        {
            anim.gameObject.transform.position += Vector3.up * -0.02f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
