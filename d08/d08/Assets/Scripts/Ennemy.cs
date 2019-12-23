using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Ennemy : Character
{

    GameObject player;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("Player");
        StartCoroutine(LookForPlayer());
    }


    IEnumerator LookForPlayer()
    {
        while (!isDead)
        {
            if (!player.GetComponent<Character>().isDead && Vector3.Distance(player.transform.position, transform.position) < detectionRange)
                attackTarget = player.transform;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void Update()
    {
        if (!isDead)
            base.Update();
    }
    public override IEnumerator Die()
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
