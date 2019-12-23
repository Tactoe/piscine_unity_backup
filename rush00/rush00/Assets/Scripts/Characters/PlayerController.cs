using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    // Start is called before the first frame update
    bool isAlive;
    void HandleInput()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        body[3].up = new Vector2(xAxis, yAxis);
        animator.SetBool("isWalking", Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0);
        transform.Translate(new Vector2(xAxis * speed * Time.deltaTime, yAxis * speed * Time.deltaTime));
        if (Input.GetKeyDown(KeyCode.E))
            TryPickingUp();
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            && weapon != null && weapon.CanAttack())
        {
            if (Input.GetMouseButton(0) && weapon.canHoldMouse)
            {
                GameCamera.gc.SetShake(weapon.shakeAmount);
                weapon.Attack();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                GameCamera.gc.SetShake(weapon.shakeAmount);
                weapon.Attack();
            }
        }
        if (Input.GetMouseButtonDown(1) && weapon != null)
        {
            weapon.Throw();
            UnequipWeapon();
        }
    }

    public override void Die()
    {
        base.Die();
        GameManager.gm.GameOver();
    }

    public override void Start()
    {
        base.Start();
        target = MouseTracker.mt.transform;
    }

    public override void Update()
    {
        base.Update();
        if (GameManager.gm.gameGoingOn)
            HandleInput();
    }

}
