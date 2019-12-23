using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public GameObject explosion;
    public int damage;
    Rigidbody rb;
    AreaDamage hasAreaDamage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction * speed, ForceMode.Impulse);
        Invoke("DestroySelf", 2.5f);
        hasAreaDamage = explosion.GetComponent<AreaDamage>();
        if (hasAreaDamage)
            explosion.GetComponent<AreaDamage>().damage = damage;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject tmp = Instantiate(explosion, transform.position, transform.rotation);
        if (collision.collider.CompareTag("Ennemy"))
            collision.collider.GetComponent<Ennemy>().GetHit(damage);
        DestroySelf();
    }
}
