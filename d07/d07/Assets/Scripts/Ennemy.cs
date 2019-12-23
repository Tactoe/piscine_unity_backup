using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Ennemy : Tank
{

    NavMeshAgent agent;
    GameObject[] tankList;
    Transform target;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        tankList = GameObject.FindGameObjectsWithTag("Tank");
        StartCoroutine("LookForTarget");
        StartCoroutine("TryAttacking");
    }

    public override void Update()
    {
        base.Update();
        if (target != null)
        {
            Quaternion rotation = Quaternion.LookRotation(target.position - tankTop.position);
            tankTop.rotation = Quaternion.RotateTowards(tankTop.rotation, rotation, 24 * Time.deltaTime);
            tankTop.rotation = Quaternion.Euler(0, tankTop.rotation.eulerAngles.y, 0);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    IEnumerator TryAttacking()
    {
        while (true)
        {
            if (Vector3.Distance(target.position, transform.position) < range
                && target.transform.position.y < transform.position.y + 1
                && target.transform.position.y > transform.position.y - 1)
                Shoot(false);
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    IEnumerator LookForTarget()
    {
        while (true)
        {
            float nearestDist = -1;
            foreach (GameObject tank in tankList)
            {
                if (tank == gameObject || tank == null)
                    continue;
                float dist = Vector3.Distance(tank.transform.position, transform.position);
                if (nearestDist == -1 || dist < nearestDist)
                {
                    nearestDist = dist;
                    target = tank.transform;
                    agent.destination = tank.transform.position;
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
