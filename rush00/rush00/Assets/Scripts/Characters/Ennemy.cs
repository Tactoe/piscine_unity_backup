using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : Character
{

    enum State { Patrolling, Chasing, LostSight, OnGuard };
    State currentState = State.Patrolling;
    public Transform pathTransform;
    public float pauseTime;
    public float sight;
    public float attackSpeed;

    GameObject playerRef;
    Transform[] path;
    Vector2 goTo;
    GameObject tmpTarget;
    Transform playerLastPos;
    //Vector2 playerLastPos;
    int nextPointIndex = 1;
    float pauseTimer;
    bool goingBack;
    // Start is called before the first frame update
    void GoToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, goTo, speed * Time.deltaTime);
    }

    public override void Start()
    {
        base.Start();
        tmpTarget = new GameObject();
        Transform[] tmp = pathTransform.transform.GetComponentsInChildren<Transform>();
        path = new Transform[tmp.Length - 1];
        for (int i = 1; i < tmp.Length; i++)
            path[i - 1] = tmp[i];
        playerRef = GameObject.FindWithTag("Player");
        target = path[1];
        goTo = target.position;
        transform.position = path[0].position;
        StartCoroutine("LookForPlayer");
    }


    public override void Die()
    {
        base.Die();
        Destroy(this);
    }

    void UpdatePath()
    {
        if (nextPointIndex - 1 >= 0 && nextPointIndex + 1 < path.Length)
            nextPointIndex += goingBack ? -1 : 1;
        else
        {
                goingBack = !goingBack;
                nextPointIndex += goingBack ? -1 : 1;
        }
        target = path[nextPointIndex];
        goTo = target.position;
        animator.SetBool("isWalking", true);
    }

    public void EnnemyBehavior()
    {
        if (currentState != State.OnGuard)
        {
            if (Vector2.Distance(transform.position, goTo) < 0.2f)
            {
                animator.SetBool("isWalking", false);
                if (currentState == State.LostSight)
                {
                    currentState = State.OnGuard;
                    StartCoroutine("LookAround");
                }
                else if (pauseTime > 0)
                    Invoke("UpdatePath", pauseTime);
                else
                    UpdatePath();
            }
            GoToTarget();
        }
    }

    bool canSeePlayer()
    {
        Vector2 raycastDir = playerRef.transform.position - transform.position;
        Vector2 lookingAt = target.position - transform.position;
        float castDist = Vector2.Dot(lookingAt, raycastDir) > 0 ? sight : sight / 10;
        Debug.DrawRay(transform.position, raycastDir.normalized * castDist, Color.black, 10);
        RaycastHit2D ok = Physics2D.Raycast(transform.position, raycastDir.normalized, castDist, LayerMask.GetMask("Characters", "Background"));
        if (ok.collider != null)
            return ok.collider.CompareTag("Player");
        return false;
    }

    IEnumerator LookForPlayer()
    {
        while (true)
        {
            if (canSeePlayer())
            {
                target = playerRef.transform;
                goTo = target.position;
                if (currentState != State.Chasing)
                {
                    currentState = State.Chasing;
                    animator.SetBool("isWalking", true);
                    StartCoroutine("AttackPlayer");
                }
            }
            else if (currentState == State.Chasing)
            {
                StopCoroutine("AttackPlayer");
                currentState = State.LostSight;
                tmpTarget.transform.position = goTo;
                target = tmpTarget.transform;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator LookAround()
    {
        while (currentState == State.OnGuard)
        {
            tmpTarget.transform.position = (Vector2)transform.position + new Vector2(Random.Range(-2, 2), Random.Range(-2, 2));
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(attackSpeed * 2);
        while (currentState == State.Chasing)
        {
            weapon.Attack();
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    public override void Update()
    {
        base.Update();
        EnnemyBehavior();
    }
}
