using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage;
    public float bulletSpeed;
    public float attackSpeed;
    public GameObject bullet;
    public bool canShoot = true;

    Animator anim;
    AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        canShoot = true;
    }

    public void Shoot()
    {
        GetComponent<AudioSource>().Play();
        GameObject tmp = Instantiate(bullet);
        tmp.transform.position = transform.position + Vector3.up * 0.2f;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(1000);
        tmp.GetComponent<Bullet>().direction = (targetPoint - transform.position).normalized;
        tmp.GetComponent<Bullet>().damage = damage;
        tmp.GetComponent<Bullet>().speed = bulletSpeed;
        anim.SetTrigger("shoot");
        canShoot = false;
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(attackSpeed);
        canShoot = true;
    }
}
