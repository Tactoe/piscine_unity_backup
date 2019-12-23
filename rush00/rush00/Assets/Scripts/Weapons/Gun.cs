using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public GameObject bulletType;
    public int ammo;
    public float spread;
    public int bulletShot;
    public float bulletSpeed;
    public float bulletLifeSpawn;

    public override bool CanAttack()
    {
        return (ammo > 0 && attackTimer <= 0);
    }

    public override void Attack()
    {
        for (int i = 0; i < bulletShot; i++)
        {
            Bullet firedBullet = Instantiate(bulletType).GetComponent<Bullet>();
            firedBullet.transform.position = Vector2.one * gameObject.transform.position;
            firedBullet.dealsDamageTo = dealsDamageTo;
            firedBullet.speed = bulletSpeed;
            firedBullet.lifeSpawn = bulletLifeSpawn;
            //firedBullet.target = (Vector2)Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition) + new Vector2(Random.Range(-spread, spread), Random.Range(-spread, spread));
            firedBullet.target = (Vector2)(transform.position - user.transform.position)
                + new Vector2(Random.Range(-spread, spread), Random.Range(-spread, spread));
        }
        ammo -= bulletShot;
        base.Attack();
    }

    public override void Awake()
    {
        base.Awake();
        spread *= 0.1f;
    }
}
