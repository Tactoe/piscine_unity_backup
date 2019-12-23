using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeSpawn;
    [HideInInspector]public Vector2 target;
    public List<string> dealsDamageTo;
    // Start is called before the first frame update
    Vector2 direction;
    void Start()
    {
        direction = target;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        lifeSpawn -= Time.deltaTime;
        transform.Translate(new Vector2(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime));
        if (lifeSpawn <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dealsDamageTo.Contains(collision.gameObject.tag))
        {
            collision.gameObject.GetComponent<Character>().Die();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
