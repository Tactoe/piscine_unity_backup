using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Ennemy : MonoBehaviour
{

    public float detectionRange;
    public int level;
    public bool isDead { private set; get; }
    public int health;
    public int maxHealth;
    public int damage;


    //bool loopAttack;
    //bool canAttack = true;
    NavMeshAgent agent;
    Animator anim;
    Transform target;
    float attackRange;
    float attackTargetDist;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        attackRange = agent.stoppingDistance + 1;
        target = GameObject.FindWithTag("MapCenter").transform;
        player = GameObject.FindWithTag("Player");
        StartCoroutine(LookForPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (health <= 0 && !isDead)
            {
                GetComponent<CapsuleCollider>().enabled = false;
                agent.isStopped = true;
                anim.SetTrigger("die");
                isDead = true;
            }
            if (target != null)
                agent.destination = target.position;
            attackTargetDist = Vector3.Distance(agent.destination, transform.position);
            anim.SetBool("isRunning", (attackTargetDist > attackRange));
            anim.SetFloat("targetDist", attackTargetDist);
            anim.SetFloat("velx", Mathf.Abs(agent.velocity.x));
            anim.SetFloat("vely", Mathf.Abs(agent.velocity.z));
        }
    }

    public void GetHit(int damage)
    {
        health -= damage;
        agent.destination = player.transform.position;
    }

    public void DealDamageToTarget()
    {
        if (target != null)
        {
            Debug.Log("Anout to dkk" + target.gameObject.GetComponent<Player>().health);
            target.gameObject.GetComponent<Player>().health -= damage;
            Debug.Log("dealt damage" + target.gameObject.GetComponent<Player>().health);
        }
    }

    IEnumerator LookForPlayer()
    {
        while (!isDead)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < attackRange)
            {
                anim.SetBool("isRunning", false);
                anim.SetTrigger("attack");
                transform.LookAt(player.transform);
            }
            else if (Vector3.Distance(player.transform.position, transform.position) < detectionRange)
            {
                target = player.transform;
            }
            else if (target != null)
            {
                agent.destination = player.transform.position;
                target = null;
            }
            agent.isStopped = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack");
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void StartDeathCoroutine()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        while (anim.gameObject.transform.position.y > -3)
        {
            anim.gameObject.transform.position += Vector3.up * -0.02f;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }
}
