using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Player : Character
{
    public Vector3 cameraOffset;
    public int xpForNextLevel;

    void HandleInput()
    {
        Camera.main.transform.position = transform.position + cameraOffset;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (hit.collider.CompareTag("Ennemy"))
                {
                    attackTarget = hit.collider.transform;
                    canAttack = true;
                }
                else
                {
                    //anim.SetBool("isRunning", true);
                    attackTarget = null;
                    agent.destination = hit.point;
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            canAttack = true;
            anim.SetBool("canAttack", true);
            //if (attackTargetDist < attackRange && attackTarget != null)
            //attackTarget = null;
        }

    }
    public override void Update()
    {
        if (!isDead)
        {
            base.Update();
            HandleInput();
            attackTargetDist = Vector3.Distance(agent.destination, transform.position);
            anim.SetBool("isRunning", (attackTargetDist > attackRange));
        }
    }
}
