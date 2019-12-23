using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public float maxHealth;
    public float speed;
    public float rotationSpeed;
    public float boostBonus;
    public float boostDuration;
    public float boostReloadTime;
    public float range;
    public float damage;
    public int missileAmmo;
    public GameObject missileExplosion;
    public GameObject bulletImpact;

    public Transform tankTop;
    public Transform tankBottom;

    protected bool canUseBoost = true;
    protected float health;
    // Start is called before the first frame update
    public virtual void Start()
    {
        health = maxHealth;
    }

    protected void Shoot(bool missile)
    {
        RaycastHit hit;
        GameObject toInstantiate = missile ? missileExplosion : bulletImpact;

        if (Physics.Raycast(tankTop.position, tankTop.forward, out hit, range))
        {
            if (hit.collider.CompareTag("Tank"))
                hit.collider.GetComponent<Tank>().health -= missile ? damage * 3 : damage;
            Instantiate(toInstantiate, hit.point, Quaternion.identity);
        }
        else
        {
            Instantiate(toInstantiate, tankTop.position + tankTop.forward * range, Quaternion.identity);
        }

    }

    public virtual void Die()
    {
        Instantiate(missileExplosion, transform.position, Quaternion.identity);
        Destroy(tankTop.gameObject);
        Destroy(tankBottom.gameObject);
        Destroy(this);
        Destroy(gameObject);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (health <= 0)
            Die();
    }

    protected IEnumerator Boost()
    {
        canUseBoost = false;
        speed += boostBonus;
        yield return new WaitForSeconds(boostDuration);
        speed -= boostBonus;
        yield return new WaitForSeconds(boostReloadTime - boostDuration);
        canUseBoost = true;
    }
}
